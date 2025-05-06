using Microsoft.AspNetCore.Mvc;
using EShop.Application;
using EShop.Domain.Models;

namespace EShopService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet("{cartId}")]
    public ActionResult<ShoppingCart> GetCart(Guid cartId)
    {
        var cart = _shoppingCartService.GetCart(cartId);
        return Ok(cart);
    }

    [HttpPost("{cartId}/add")]
    public IActionResult AddToCart(Guid cartId, [FromBody] CartItem item)
    {
        _shoppingCartService.AddToCart(cartId, item.ProductId, item.Quantity);
        return Ok();
    }

    [HttpDelete("{cartId}/remove/{productId}")]
    public IActionResult RemoveFromCart(Guid cartId, int productId)
    {
        _shoppingCartService.RemoveFromCart(cartId, productId);
        return Ok();
    }
}
