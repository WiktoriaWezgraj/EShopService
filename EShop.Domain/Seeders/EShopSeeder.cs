using System.Collections.Generic;
using EShop.Domain.Models;

namespace EShop.Domain.Seeders;

//asynchronicznie musi wrzucać do bazy danych

public static class EShopSeeder
{
    public static List<Category> GetInitialCategories()
    {
        return
        [
            new Category { Id = 1, Name = "Kawy" },
            new Category { Id = 2, Name = "Herbaty" },
            new Category { Id = 3, Name = "Akcesoria" },
        ];
    }

    public static List<Product> GetInitialProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = 1,
                Name = "Kawa ziarnista arabica 1 kg",
                Ean = "1234567890123",
                Price = 49.99m,
                Stock = 100,
                Sku = "SKU01",
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Name = "Zielona herbata matcha 100g",
                Ean = "123456789012",
                Price = 19.99m,
                Stock = 200,
                Sku = "SKU02",
                CategoryId = 2
            },
            new Product
            {
                Id = 3,
                Name = "Spieniacz do mleka ręczny",
                Ean = "1234567890125",
                Price = 9.99m,
                Stock = 300,
                Sku = "SKU03",
                CategoryId = 3
            },
        };
    }
}