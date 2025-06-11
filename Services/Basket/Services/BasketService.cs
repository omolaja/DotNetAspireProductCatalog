
using Basket.ApiClient;
using Microsoft.AspNetCore.Http.Features;

namespace Basket.Services
{
    public class BasketService(IDistributedCache cache, ProductCatalogClient productCatalogClient)
    {
        public async Task<ShoppingCart?> GetBasket(string userId)
        {
            var basket = await cache.GetStringAsync(userId);
            return string.IsNullOrEmpty(basket) ? null : JsonSerializer.Deserialize<ShoppingCart>(basket);
        }

        //public async Task UpdateBasket(ShoppingCart basket)
        //{
        //    await cache.SetStringAsync(basket.UserId, JsonSerializer.Serialize<ShoppingCart>(basket));
        //}
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

            var existingCartJson = await cache.GetStringAsync(incomingCart.UserId);

            ShoppingCart cart;

            if (string.IsNullOrEmpty(existingCartJson))
            {
                // No cart exists, store the new one
                cart = incomingCart;
            }
            else
            {
                cart = JsonSerializer.Deserialize<ShoppingCart>(existingCartJson) ?? new ShoppingCart { UserId = incomingCart.UserId };

                foreach (var newItem in incomingCart.Items)
                {
                    var existingItem = cart.Items.FirstOrDefault(i =>
                        i.ProductId == newItem.ProductId &&
                        i.Color == newItem.Color // add more matching logic if needed
                    );

                    if (existingItem != null)
                    {
                        // Update quantity and timestamp
                        existingItem.Quantity += newItem.Quantity; // or += if you want to accumulate
                        existingItem.Price = newItem.Price; // optionally update price
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
            await cache.SetStringAsync(cart.UserId, updatedCartJson);
        }

        public async Task DeleteBasket(string userId)
        {
            await cache.RemoveAsync(userId);
        }
    }
}
