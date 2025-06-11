namespace ProductCatalog.Models
{
    public class Product
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = default;

        public string ProductDescription { get; set; } = default;

        public decimal Price { get; set; }

        public string ProductImage { get; set; } = default;
    }
}
