namespace Basket.Models
{
    public class ShoppingCartItem
    {
        public int Id { get; set; }
        public int ProductId { get; set; } = default!;
        public string ProductName { get; set; } = default!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public string Color { get; set; } = default!;
        //public string Size { get; set; } = default!;
        //public string Material { get; set; } = default!;
        //public string Name { get; set; } = default!;
        //public string Sku { get; set; } = default!;
        //public string Tags { get; set; } = default!;
        //public string ProductType { get; set; } = default!;
        //public string ProductUrl { get; set; } = default!; 
    }
}
