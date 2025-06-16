
using Basket.ApiClient;
using Microsoft.AspNetCore.Http.Features;
using StackExchange.Redis;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache, ProductCatalogClient productCatalogClient, IConnectionMultiplexer redis)
    {
        public async Task<ShoppingCart?> GetBasket(string userId)
        {
            var key = $"basket:{userId}";
            var basket = await cache.GetStringAsync(key);
            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

       
        public async Task UpdateBasket(ShoppingCart incomingCart)
        {

            foreach(var item in incomingCart.Items)
            {
                // Fetch product details from ProductCatalogClient
                var product = await productCatalogClient.GetProductById(item.ProductId);
                if (product != null)
                {
                    // Update item price based on product details
                    item.Price = product.Price;
                    item.ProductName = product.ProductName;
                }
            }

            var key = $"basket:{incomingCart.UserId}";
            var existingCartJson = await cache.GetStringAsync(key);

            ShoppingCart cart;

            if (string.IsNullOrEmpty(existingCartJson))
            {
               
                cart = incomingCart;
            }
            else
            {
                cart = JsonSerializer.Deserialize<ShoppingCart>(existingCartJson) ?? new ShoppingCart { UserId = incomingCart.UserId };

                foreach (var newItem in incomingCart.Items)
                {
                    var existingItem = cart.Items.FirstOrDefault(i =>
                        i.ProductId == newItem.ProductId &&
                        i.Color == newItem.Color 
                    );

                    if (existingItem != null)
                    {
                        // Update item
                        existingItem.Quantity += newItem.Quantity; 
                        existingItem.Price = newItem.Price;
                        existingItem.UpdatedAt = DateTime.UtcNow;
                    }
                    else
                    {
                        // Add new item
                        newItem.CreatedAt = DateTime.UtcNow;
                        newItem.UpdatedAt = DateTime.UtcNow;
                        cart.Items.Add(newItem);
                    }
                }
            }

            var updatedCartJson = JsonSerializer.Serialize(cart);
            key = $"basket:{cart.UserId}";
            await cache.SetStringAsync(key, updatedCartJson);
        }

        public async Task DeleteBasket(string userId)
        {
            await cache.RemoveAsync(userId);
        }

        internal async Task UpdateProductPriceInAllBasketsAsync(int productId, decimal price)
        {
            var db = redis.GetDatabase();
            var server = redis.GetServer(redis.GetEndPoints().First());
            await foreach (var key in server.KeysAsync(pattern: "*"))
            {
                string keyString = key.ToString() ?? string.Empty;
                var basket = await GetBasket(keyString.Replace("basket:",""));
                var basketItem = basket?.Items.FirstOrDefault(i => i.ProductId == productId);
                if (basketItem != null)
                {
                    basketItem.Price = price;
                    
                    await cache.SetStringAsync(keyString, JsonSerializer.Serialize(basket));

                }
                else
                {
                    throw new Exception($"Product with ID {productId} not found in the basket.");
                }
            }
        }
    }
}
