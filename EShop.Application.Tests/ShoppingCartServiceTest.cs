using System;
using System.Linq;
using EShop.Application;
using Xunit;

namespace EShop.Application.Tests
{
    public class ShoppingCartServiceTests
    {
        private readonly ShoppingCartService _service = new();

        [Fact]
        public void AddToCart_ShouldAddItem()
        {
            var cartId = Guid.NewGuid();
            _service.AddToCart(cartId, 1, 2);

            var cart = _service.GetCart(cartId);
            Assert.Single(cart.Items);
            Assert.Equal(1, cart.Items[0].ProductId);
            Assert.Equal(2, cart.Items[0].Quantity);
        }

        [Fact]
        public void RemoveFromCart_ShouldRemoveItem()
        {
            var cartId = Guid.NewGuid();
            _service.AddToCart(cartId, 1, 2);
            _service.RemoveFromCart(cartId, 1);

            var cart = _service.GetCart(cartId);
            Assert.Empty(cart.Items);
        }

        [Fact]
        public void GetCart_ShouldReturnEmptyCart_WhenNotExists()
        {
            var cartId = Guid.NewGuid();
            var cart = _service.GetCart(cartId);
            Assert.NotNull(cart);
            Assert.Empty(cart.Items);
        }
    }
}
