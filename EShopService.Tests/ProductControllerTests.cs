using Moq;
using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShopService.Controllers;
using EShop.Domain.Models;
using Microsoft.Extensions.Caching.Distributed;

namespace EShopService.Tests.Controllers;

public class ProductControllerTests
{
    private readonly Mock<IProductService> _mockService;
    private readonly ProductController _controller;
    private readonly Mock<IDistributedCache> _mockCache;


    public ProductControllerTests()
    {
        _mockService = new Mock<IProductService>();
        _mockCache = new Mock<IDistributedCache>();
        _controller = new ProductController(_mockService.Object, _mockCache.Object);
    }


    [Fact]
    public async Task Get_ShouldReturnAllProducts_ReturnTrue()
    {
        var products = new List<Product> { new Product(), new Product() };
        _mockService.Setup(s => s.GetAllAsync()).ReturnsAsync(products);

        var result = await _controller.Get();

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(products, okResult.Value);
    }

    [Fact]
    public async Task Get_WithValidId_ReturnsProduct_ReturnTrue()
    {
         var product = new Product { Id = 1 };
        _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);

        var result = await _controller.Get(1);

        var okResult = Assert.IsType<OkObjectResult>(result);
        Assert.Equal(product, okResult.Value);
    }

    [Fact]
    public async Task Get_WithInvalidId_ReturnsNotFound()
    { 
        _mockService.Setup(s => s.GetAsync(It.IsAny<int>())).ReturnsAsync((Product)null);

        var result = await _controller.Get(999);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Post_ValidProduct_ReturnsCreatedProduct()
    {
        var newProduct = new Product();
        _mockService.Setup(s => s.AddAsync(It.IsAny<Product>())).ReturnsAsync(newProduct);

        var result = await _controller.Post(newProduct);

        _mockService.Verify(s => s.AddAsync(newProduct), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Put_ValidProduct_UpdatesAndReturnsOk()
    {
        var product = new Product { Id = 1 };
        _mockService.Setup(s => s.UpdateAsync(product)).ReturnsAsync(product);

        var result = await _controller.Put(1, product);

        _mockService.Verify(s => s.UpdateAsync(product), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Delete_ValidId_MarksDeletedAndUpdates()
    {
        var product = new Product { Id = 1, Deleted = false };
        _mockService.Setup(s => s.GetAsync(1)).ReturnsAsync(product);
        _mockService.Setup(s => s.UpdateAsync(It.IsAny<Product>())).ReturnsAsync(product);

        var result = await _controller.Delete(1);

        _mockService.Verify(s => s.GetAsync(1), Times.Once);
        _mockService.Verify(s => s.UpdateAsync(It.Is<Product>(p => p.Deleted)), Times.Once);
        Assert.IsType<OkObjectResult>(result);
    }
}