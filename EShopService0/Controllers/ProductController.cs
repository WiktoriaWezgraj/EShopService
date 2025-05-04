using EShop.Application;
using EShop.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMemoryCache _cache;
        private const string ProductCacheKey = "product_list";

        public ProductController(IProductService productService, IMemoryCache cache)
        {
            _productService = productService;
            _cache = cache;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            if (!_cache.TryGetValue(ProductCacheKey, out List<Product> result))
            {
                result = await _productService.GetAllAsync();

                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24)
                };

                _cache.Set(ProductCacheKey, result, cacheEntryOptions);
            }

            return Ok(result);
        }

        // GET api/<ProductController>/5
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

        // POST api/<ProductController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Product product)
        {
            var result = await _productService.AddAsync(product);
            _cache.Remove(ProductCacheKey);
            return Ok(result);
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] Product product)
        {
            var result = await _productService.UpdateAsync(product);
            _cache.Remove(ProductCacheKey);
            return Ok(result);
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _productService.GetAsync(id);
            product.Deleted = true;
            var result = await _productService.UpdateAsync(product);
            _cache.Remove(ProductCacheKey);
            return Ok(result);
        }

        [HttpPatch]
        public ActionResult Add([FromBody] Product product)
        {
            var result = _productService.Add(product);
            _cache.Remove(ProductCacheKey);
            return Ok(result);
        }
    }
}
