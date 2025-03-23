using EShop.Application;
using EShop.Domain.Enums;
using EShop.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
namespace EShopService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreditCardController : ControllerBase
    {
        [HttpGet("{cardNumber}")]
        public IActionResult ValidateCard(string cardNumber)
        {
            var credit = new CreditCardService();
            try
            {
                credit.ValidateCard(cardNumber);
            }
            catch (CardNumberTooLongException)
            {
                return StatusCode(414);
            }
            catch (CardNumberTooShortException)
            {
                return StatusCode(400);
            }
            catch (CardNumberInvalidException)
            {
                return StatusCode(400);
            }

            CreditCardProvider? provider = credit.GetCardType(cardNumber);
            if (provider == null)
            {
                return StatusCode(406);
            }
            return Ok(new { Status = "Valid", Provider = provider.Value });
        }
    }
}
