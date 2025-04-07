
using EShop.Domain.Models;

namespace EShop.Application;

public interface IProductService
{
    List<string> GetProductNames();
    void Add(Product product);
    void Update(Product product);
    void Delete(int id);
}