using CreditCardValidation.Services.Common;
using Xunit;

namespace CreditCardValidation.Tests.Services
{

    public class LuhnValidatorTests
    {

        [Theory]
        [InlineData("12345678910")]
        [InlineData("123")]
        [InlineData("0")]
        [InlineData("999999999999999999")]
        [InlineData("444444448888888889")]
        [InlineData("4444444488//344?9")]
        public void ValidateCardNumber_WhenNumberIsInvalid_ReturnsFalse(string number)
        {
            Assert.False(LuhnValidator.IsValidCardNumber(number));
        }

        [Theory]
        [InlineData("4915205476757332")]
        [InlineData("4915197626180459")]
        [InlineData("5577000055770004")]
        [InlineData("4915212892031284")]
        [InlineData("4915195781996115")]
        public void ValidateCardNumber_WhenNumberIsValidAndEven_ReturnsTrue(string number)
        {
            Assert.True(LuhnValidator.IsValidCardNumber(number));
        }

        [Theory]
        [InlineData("4005617731711")]
        [InlineData("101631161953856")]
        [InlineData("101650580782126")]
        [InlineData("101661919493533")]
        public void ValidateCardNumber_WhenNumberIsValidAndOdd_ReturnsTrue(string number)
        {
            Assert.True(LuhnValidator.IsValidCardNumber(number));
        }
    }
}
