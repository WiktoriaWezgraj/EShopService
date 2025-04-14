using EShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Domain.Repositories;

public interface IProductsRepository
{
    List<Product> GetAllProducts();
    void AddProduct(Product product);
    void UpdateProduct(Product product);
    void DeleteProduct(int id);
}
