﻿using EShop.Domain.Models;
using EShop.Domain.Repositories;

namespace EShop.Application
{
    public class ProductService : IProductService
    {
        private IProductsRepository _repository;
        public ProductService(IProductsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var result = await _repository.GetAllProductAsync();

            return result;
        }

        public async Task<Product> GetAsync(int id)
        {
            var result = await _repository.GetProductAsync(id);

            return result;
        }

        public async Task<Product> Update(Product product)
        {
            var result = await _repository.UpdateProductAsync(product);

            return result;
        }

        public async Task<Product> Add(Product product)
        {
            var result = await _repository.AddProductAsync(product);

            return result;
        }
    }
}