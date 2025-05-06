namespace EShop.Domain.Models
{
    public class ShoppingCart
    {
        public Guid Id { get; set; } = Guid.NewGuid(); //id to guid
        public List<CartItem> Items { get; set; } = new List<CartItem>();
    }
}

