using EShop.Domain.Models;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace EShop.Domain.Repositories;

public class ProductsRepository
{
    private readonly DataContext _dataContext;

    public ProductsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<Product> GetAllProducts()
    {
        return _dataContext.Products.ToList();
    }

    public void AddProduct(Product product)
    {
        _dataContext.Products.Add(product);
        _dataContext.SaveChanges();
    }

    public void UpdateProduct(Product product)
    {
        _dataContext.Products.Update(product);
        _dataContext.SaveChanges();
    }

    public void DeleteProduct(int id)
    {
        var product = _dataContext.Products.Find(id);
        if (product != null)
        {
            _dataContext.Products.Remove(product);
            _dataContext.SaveChanges();
        }
    }
}
