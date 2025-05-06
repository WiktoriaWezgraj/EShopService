using EShop.Domain.Models;

namespace EShop.Application
{
    public interface IShoppingCartService
    {
        void AddToCart(Guid cartId, int productId, int quantity);
        void RemoveFromCart(Guid cartId, int productId);
        ShoppingCart GetCart(Guid cartId);
    }
}
