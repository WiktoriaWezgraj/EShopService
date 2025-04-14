using Moq;
using Xunit;
using EShopService.Controllers;
using EShop.Application;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EShopService.Tests;

public class ProductControllerTests
{
    [Fact]
    public void Get_ReturnsProductNames()
    {
        // Arrange
        var mockProductService = new Mock<IProductService>();
        var expectedProductNames = new List<string> { "Product1", "Product2" };

        // Konfiguracja mocka, aby zwróci³ listê nazw produktów
        mockProductService.Setup(service => service.GetProductNames()).Returns(expectedProductNames);

        // Tworzymy instancjê kontrolera z zamockowanym serwisem
        var controller = new ProductController(mockProductService.Object);

        // Act
        var result = controller.Get();  // Wywo³anie metody Get() kontrolera

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);  // Sprawdzamy, czy wynik to OkObjectResult
        var productNames = Assert.IsType<List<string>>(okResult.Value);  // Sprawdzamy, czy wynik to lista stringów
        Assert.Equal(expectedProductNames.Count, productNames.Count);  // Sprawdzamy, czy liczba produktów jest zgodna
        Assert.Equal(expectedProductNames, productNames);  // Sprawdzamy, czy nazwy produktów s¹ takie same
    }

    [Fact]
    public void Get_ById_ReturnsProductValue()
    {
        // Arrange
        var mockProductService = new Mock<IProductService>();
        mockProductService.Setup(service => service.GetProductNames()).Returns(new List<string> { "Product1" });

        var controller = new ProductController(mockProductService.Object);

        // Act
        var result = controller.Get(1);  // Wywo³anie metody Get(int id)

        // Assert
        Assert.Equal("value", result);  // Sprawdzamy, czy zwrócona wartoœæ to "value"
    }
}
