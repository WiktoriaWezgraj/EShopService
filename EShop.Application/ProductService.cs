using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application;

public class ProductService : IProductService
{
    public readonly IProductsRepository _productsRepository;

    public ProductService(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public List<string> GetProductNames()
    {
        var products = _productsRepository.GetAllProducts();
        return products.Select(p => p.Name).ToList();
    }

    public void Add(Product product)
    {
        _productsRepository.AddProduct(product);
    }

    public void Update(Product product)
    {
        _productsRepository.UpdateProduct(product);
    }

    public void Delete(int id)
    {
        _productsRepository.DeleteProduct(id);
    }
}



