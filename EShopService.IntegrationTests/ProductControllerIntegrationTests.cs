using EShop.Application;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

public class ProductControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;
    private readonly Mock<IProductService> _productServiceMock;

    public ProductControllerTests(WebApplicationFactory<Program> factory)
    {
        _productServiceMock = new Mock<IProductService>();

        var customizedFactory = factory.WithWebHostBuilder(builder =>
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(d => d.ServiceType == typeof(IProductService));
                if (descriptor != null)
                    services.Remove(descriptor);

                services.AddSingleton(_productServiceMock.Object);
            });
        });

        _client = customizedFactory.CreateClient();
    }

    [Fact]
    public async Task Get_ReturnsProductNames()
    {
        // Arrange
        var expected = new List<string> { "Apple", "Banana" };
        _productServiceMock.Setup(s => s.GetProductNames()).Returns(expected);

        // Act
        var response = await _client.GetAsync("/api/product");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var content = await response.Content.ReadAsStringAsync();
        var productNames = JsonSerializer.Deserialize<List<string>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        Assert.NotNull(productNames);
        Assert.Contains("Apple", productNames);
        Assert.Contains("Banana", productNames);
    }

    [Fact]
    public async Task Post_CallsAddMethod()
    {
        // Arrange
        var product = new Product { Id = 1, Name = "TestProduct", Price = 10 };
        var json = JsonSerializer.Serialize(product.Name); // kontroler przyjmuje string, nie obiekt

        // Act
        var response = await _client.PostAsync("/api/product", new StringContent(json, Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // kontroler nie zwraca nic, ale domyœlnie zwróci 200
        _productServiceMock.Verify(s => s.Add(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Put_CallsUpdateMethod()
    {
        // Arrange
        var product = new Product { Id = 5, Name = "UpdatedProduct", Price = 15 };
        var json = JsonSerializer.Serialize(product.Name);

        // Act
        var response = await _client.PutAsync("/api/product/5", new StringContent(json, Encoding.UTF8, "application/json"));

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _productServiceMock.Verify(s => s.Update(It.IsAny<Product>()), Times.Once);
    }

    [Fact]
    public async Task Delete_CallsDeleteMethod()
    {
        // Act
        var response = await _client.DeleteAsync("/api/product/7");

        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        _productServiceMock.Verify(s => s.Delete(7), Times.Once);
    }
}
