using EShop.Application;
using Microsoft.AspNetCore.Mvc;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EShopService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService; 
    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    // GET: api/<ProductController>
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return _productService.GetProductNames();
    }

    // GET api/<ProductController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ProductController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ProductController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ProductController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
