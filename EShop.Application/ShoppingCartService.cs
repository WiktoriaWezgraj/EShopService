using EShop.Domain.Models;

namespace EShop.Application
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly Dictionary<Guid, ShoppingCart> _carts = new();

        public void AddToCart(Guid cartId, int productId, int quantity)
        {
            if (!_carts.TryGetValue(cartId, out var cart))
            {
                cart = new ShoppingCart();
                _carts[cartId] = cart;
            }

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
                item.Quantity += quantity;
            else
                cart.Items.Add(new CartItem { ProductId = productId, Quantity = quantity });
        }

        public void RemoveFromCart(Guid cartId, int productId)
        {
            if (_carts.TryGetValue(cartId, out var cart))
            {
                cart.Items.RemoveAll(i => i.ProductId == productId);
            }
        }

        public ShoppingCart GetCart(Guid cartId)
        {
            return _carts.TryGetValue(cartId, out var cart) ? cart : new ShoppingCart { Id = cartId };
        }
    }

}
