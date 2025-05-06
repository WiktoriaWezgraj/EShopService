using EShop.Application;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IDistributedCache _cache;
        private const string ProductCacheKey = "product_list";

        public ProductController(IProductService productService, IDistributedCache cache)
        {
            _productService = productService;
            _cache = cache;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<Product> result;

            var cachedData = await _cache.GetStringAsync(ProductCacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                result = JsonSerializer.Deserialize<List<Product>>(cachedData);
            }
            else
            {
                result = await _productService.GetAllAsync();

                var options = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                };

                var serialized = JsonSerializer.Serialize(result);
                await _cache.SetStringAsync(ProductCacheKey, serialized, options);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _productService.GetAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            var result = await _productService.AddAsync(product);
            await _cache.RemoveAsync(ProductCacheKey);
            return Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            var result = await _productService.UpdateAsync(product);
            await _cache.RemoveAsync(ProductCacheKey);
            return Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetAsync(id);
            if (product == null)
                return NotFound();

            product.Deleted = true;
            var result = await _productService.UpdateAsync(product);
            await _cache.RemoveAsync(ProductCacheKey);
            return Ok(result);
        }

        [Authorize(Policy = "EmployeeOnly")]
        [HttpPatch]
        public async Task<ActionResult> Add([FromBody] Product product)
        {
            var result = await _productService.AddAsync(product);
            await _cache.RemoveAsync(ProductCacheKey);
            return Ok(result);
        }
    }
}
