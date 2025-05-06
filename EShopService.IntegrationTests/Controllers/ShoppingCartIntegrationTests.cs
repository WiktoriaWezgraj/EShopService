using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System;

namespace EShopService.IntegrationTests.Controllers
{
    public class ShoppingCartControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public ShoppingCartControllerIntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task AddAndGetCart_ShouldReturnCartWithItems()
        {
            var cartId = Guid.NewGuid();
            var item = new CartItem { ProductId = 1, Quantity = 2 };

            var addResponse = await _client.PostAsJsonAsync($"/api/ShoppingCart/{cartId}/add", item);
            addResponse.EnsureSuccessStatusCode();

            var cart = await _client.GetFromJsonAsync<ShoppingCart>($"/api/ShoppingCart/{cartId}");
            Assert.NotNull(cart);
            Assert.Single(cart.Items);
            Assert.Equal(1, cart.Items[0].ProductId);
            Assert.Equal(2, cart.Items[0].Quantity);
        }

        [Fact]
        public async Task RemoveFromCart_ShouldReturnEmptyCart()
        {
            var cartId = Guid.NewGuid();
            var item = new CartItem { ProductId = 1, Quantity = 2 };

            await _client.PostAsJsonAsync($"/api/ShoppingCart/{cartId}/add", item);
            await _client.DeleteAsync($"/api/ShoppingCart/{cartId}/remove/1");

            var cart = await _client.GetFromJsonAsync<ShoppingCart>($"/api/ShoppingCart/{cartId}");
            Assert.NotNull(cart);
            Assert.Empty(cart.Items);
        }
    }
}
