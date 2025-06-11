namespace Basket.Models
{
    public class ShoppingCart
    {
        public string UserId { get; set; } = default;

        public List<ShoppingCartItem> Items { get; set; } = new List<ShoppingCartItem>();

        public decimal TotalPrice => Items.Sum(item => item.Price * item.Quantity);
    }
}
