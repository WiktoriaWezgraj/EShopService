using EShop.Application;
using EShop.Domain.Exceptions;

namespace EShop.Domain.Tests
{
    public class CreditCardProviderTests
    {
        [Fact]
        public void GetCardType_ShouldThrowCardNumberTooShortException()
        {
            var credit = new CreditCardService();
            string cardNumber = "123456789015";
            Assert.Throws<CardNumberTooShortException>(() => credit.GetCardType(cardNumber));
        }

        [Fact]
        public void GetCardType_ShouldThrowCardNumberTooLongException()
        {
            var credit = new CreditCardService();
            string cardNumber = "12345678901234567894";
            Assert.Throws<CardNumberTooLongException>(() => credit.GetCardType(cardNumber));
        }

        [Fact]
        public void GetCardType_ShouldThrowCardNumberInvalidException()
        {
            var credit = new CreditCardService();
            string cardNumber = "00000000000000000";
            var exception = Assert.Throws<CardNumberTooLongException>(() => credit.GetCardType(cardNumber));
            Assert.Equal("Card number is invalid.", exception.Message);
        }
    }
}