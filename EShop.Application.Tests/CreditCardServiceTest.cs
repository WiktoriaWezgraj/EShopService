namespace EShop.Application.Tests
{
    public class CreditCardServiceTest
    {
        [Theory]
        [InlineData("3497 7965 8312 797")] //prawidlowy
        [InlineData("345-470-784-783-0108766")] //19 cyfr
        [InlineData("4532289052809")] // 13 cyfr

        public void ValidateCreditCardNumber_ShouldReturnTrue(string cardNumber)
        {
            var credit = new CreditCardService();
            bool result = credit.ValidateCard(cardNumber);
            Assert.Equal(result, true);
        }

        [Theory]
        [InlineData("1234 5678 9012", false)] //za krótki- 12 cyfr
        [InlineData("12345671234567123456", false)] //za d³ugi- 20 cyfr
        [InlineData("", false)]
        //[InlineData("-", false)]

        public void ValidateCreditCardNumber_ShouldReturnFalse(string cardNumber, bool expected)
        {
            var credit = new CreditCardService();
            bool result = credit.ValidateCard(cardNumber);
            Assert.Equal(result, expected);
        }

        [Theory]
        [InlineData("4000 0000 0000 0004", "Visa")] //numery zaczynaj¹ siê od 4
        [InlineData("5500 0000 0000 0000", "MasterCard")] //51-55 lub 2221-2720
        [InlineData("3400 0000 0000 000", "American Express")] //34 lub 37
        [InlineData("6011 0000 0000 0004", "Discover")] //6011, 65, 644-649
        [InlineData("3528 0000 0000 0000", "JCB")] //3528-3589
        [InlineData("3000 0000 0000 04", "Diners Club")] //300-305, 36, 38
        [InlineData("5000 0000 0000 0000", "Maestro")] //50, 56-69
        [InlineData("9999 9999 9999 9999", "Nieznany wydawca")] //nieznany numer
        public void GetCardType_ShouldReturnCorrectCardType(string cardNumber, string expected)
        {
            var credit = new CreditCardService();
            string result = credit.GetCardType(cardNumber);
            Assert.Equal(expected, result);
        }


    }
}