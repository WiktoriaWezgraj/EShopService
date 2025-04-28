using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

//maybe wrong

public class CreditCardControllerIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public CreditCardControllerIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task ValidateCard_ReturnsOk_WhenCardIsValid()
    {
        // Arrange
        var validCardNumber = "4111111111111111";  

        // Act
        var response = await _client.GetAsync($"/api/creditcard/{validCardNumber}");

        // Assert
        response.EnsureSuccessStatusCode(); 
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Valid", content); 
    }

    [Fact]
    public async Task ValidateCard_Returns414_WhenCardNumberTooLong()
    {
        // Arrange
        var longCardNumber = "41111111111111111111111111111111";  // Zbyt długi numer karty

        // Act
        var response = await _client.GetAsync($"/api/creditcard/{longCardNumber}");

        // Assert
        Assert.Equal(414, (int)response.StatusCode); 
    }

    [Fact]
    public async Task ValidateCard_Returns400_WhenCardNumberTooShort()
    {
        // Arrange
        var shortCardNumber = "4111"; 

        // Act
        var response = await _client.GetAsync($"/api/creditcard/{shortCardNumber}");

        // Assert
        Assert.Equal(400, (int)response.StatusCode);  // Sprawdzamy, czy odpowiedź ma status 400 (Bad Request)
    }

    [Fact]
    public async Task ValidateCard_Returns406_WhenCardNumberInvalid()
    {
        // Arrange
        var invalidCardNumber = "1234567890123456";  // Przykładowy niepoprawny numer karty

        // Act
        var response = await _client.GetAsync($"/api/creditcard/{invalidCardNumber}");

        // Assert
        Assert.Equal(406, (int)response.StatusCode);  // Sprawdzamy, czy odpowiedź ma status 406 (Not Acceptable)
    }

    [Fact]
    public async Task ValidateCard_Returns406_WhenCardProviderIsNull()
    {
        // Arrange
        var cardNumberWithoutProvider = "0000000000000000";  // Przykładowy numer karty, dla której nie znaleziono dostawcy

        // Act
        var response = await _client.GetAsync($"/api/creditcard/{cardNumberWithoutProvider}");

        // Assert
        Assert.Equal(406, (int)response.StatusCode); 
    }
}
