
using EventMessaging.Events;

namespace ProductCatalog.Services
{
    public class ProductService(ProductDbContext context, IBus bus )
    {
        private readonly ProductDbContext _context = context;
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
        public async Task  UpdateProductAsync(Product product, decimal oldprice, int productId)
        {

            if(product.Price != oldprice)
            {
                var priceChangedEvent = new PriceChangeIntegrationEvent
                {
                    ProductId = productId,
                    ProductDescription = product.ProductDescription,
                    ProductImage = product.ProductImage,
                    ProductName = product.ProductName,
                    Price = product.Price
                };
                await bus.Publish(priceChangedEvent);
            }
           
            _context.Products.Update(product);
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
