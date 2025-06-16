
using EventMessaging.Events;

namespace ProductCatalog.Services
{
    public class ProductService(ProductDbContext context, IBus bus )
    {
        private readonly ProductDbContext _context = context;
        private readonly IBus _bus = bus;
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }
        public async Task<Product?> GetProductByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }
        public async Task  UpdateProductAsync(Product product, Product existingProduct)
        {


            if (product.Price != existingProduct.Price)
            {
                var priceChangedEvent = new PriceChangeIntegrationEvent
                {
                    ProductId = existingProduct.Id,
                    ProductDescription = product.ProductDescription,
                    ProductImage = product.ProductImage,
                    ProductName = product.ProductName,
                    Price = product.Price
                };
                await _bus.Publish(priceChangedEvent);
            }
            existingProduct.Price = product.Price;
            existingProduct.ProductName = product.ProductName;
            existingProduct.ProductDescription = product.ProductDescription;
            existingProduct.ProductImage = product.ProductImage;

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteProductAsync(int id)
        {
            var product = await GetProductByIdAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
        }
    }
    
}
