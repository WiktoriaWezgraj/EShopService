using EShop.Domain.Models;
using EShop.Domain.Repositories;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using System.Net.Http.Json;
using Xunit.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace EShopService.IntegrationTests
{
    public class Post_AddThousandProductsAsync_ExpectedThousandProducts : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        private readonly HttpClient _httpClient;
        private readonly ITestOutputHelper _testOutputHelper;

        public Post_AddThousandProductsAsync_ExpectedThousandProducts(WebApplicationFactory<Program> factory, ITestOutputHelper testOutputHelper)
        {
            _factory = factory;
            _httpClient = _factory.CreateClient();
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task Post_AddThousandsProductsAsync_ExpectedThousandsProducts()
        {
            // Arrange
            var stopwatch = new Stopwatch();
            var products = new List<Product>();

            for (int i = 0; i < 10_000; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i}",
                    Price = 9.99M,
                    Stock = 100
                });
            }

            stopwatch.Start();

            // Act
            foreach (var product in products)
            {
                var response = await _httpClient.PostAsJsonAsync("/api/products", product);
                response.EnsureSuccessStatusCode();
            }

            stopwatch.Stop();

            // Assert
            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            var count = await dbContext.Products.CountAsync();

            Assert.Equal(10_000, count);
            _testOutputHelper.WriteLine($"Insertion time: {stopwatch.Elapsed.TotalSeconds} seconds");
        }
    }
}

