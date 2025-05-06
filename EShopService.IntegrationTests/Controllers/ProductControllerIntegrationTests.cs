using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using Xunit;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using EShop.Domain.Repositories;

namespace EShopService.IntegrationTests.Controllers
{
    public class ProductControllerIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public ProductControllerIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    var dbContextDescriptor = services
                        .SingleOrDefault(s => s.ServiceType == typeof(DbContextOptions<DataContext>));

                    if (dbContextDescriptor != null)
                        services.Remove(dbContextDescriptor);

                    services.AddDbContext<DataContext>(options =>
                        options.UseInMemoryDatabase("TestDb"));
                });
            });

            _client = _factory.CreateClient();
        }

        private void AddAuthHeaderWithRole(string role)
        {
            var rsa = RSA.Create();
            rsa.ImportFromPem(File.ReadAllText("../data/private.key"));

            var creds = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);

            var token = new JwtSecurityToken(
                issuer: "EShopNetCourse",
                audience: "Eshop",
                claims: new[] { new Claim(ClaimTypes.Role, role) },
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenString);
        }

        [Fact]
        public async Task Get_ReturnsAllProducts()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                dbContext.Products.RemoveRange(dbContext.Products);
                dbContext.Products.AddRange(
                    new Product { Name = "Product1" },
                    new Product { Name = "Product2" }
                );
                await dbContext.SaveChangesAsync();
            }

            var response = await _client.GetAsync("/api/product");
            response.EnsureSuccessStatusCode();
            var products = await response.Content.ReadFromJsonAsync<List<Product>>();
            Assert.Equal(2, products?.Count);
        }

        [Fact]
        public async Task Post_AddProduct_WithAuth()
        {
            AddAuthHeaderWithRole("Employee");

            var product = new Product { Name = "TestProduct" };
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("/api/product", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Product>();
            Assert.Equal("TestProduct", result?.Name);
        }

        [Fact]
        public async Task Patch_AddProduct_WithAuth()
        {
            AddAuthHeaderWithRole("Employee");

            var product = new Product { Name = "PatchProduct" };
            var content = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

            var response = await _client.PatchAsync("/api/product", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<Product>();
            Assert.Equal("PatchProduct", result?.Name);
        }
    }
}
