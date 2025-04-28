using EShop.Domain.Models;
using EShop.Domain.Repositories;
using System.Collections.Generic;

namespace EShop.Domain.Seeders;

public class EShopSeeder(DataContext context) : IEShopSeeder
{
    public async Task Seed()
    {
        if (!context.Products.Any())
        {
            var items = new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Product1",
                Ean = "1234567890123",
                Price = 49.99m,
                Stock = 100,
                Sku = "SKU01",
            },
            new Product
            {
                Id = 2,
                Name = "Product2",
                Ean = "123456789012",
                Price = 19.99m,
                Stock = 200,
                Sku = "SKU02",
            },
            new Product
            {
                Id = 3,
                Name = "Product3",
                Ean = "1234567890125",
                Price = 9.99m,
                Stock = 300,
                Sku = "SKU03",
            }
        };

            context.Products.AddRange(items);
            context.SaveChanges();
        }
    }
}