using CreditCardValidation.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public void ValidateCardNumber_WhenNumberIsInvalid_ReturnsFalse(string number)
        {
            Assert.False(LuhnValidator.IsValidCardNumber(number));
        }
        
        [Theory]
        [InlineData("4915205476757332")]
        [InlineData("4915197626180459")]
        [InlineData("4915202062685977")]
        [InlineData("4915212892031284")]
        [InlineData("4915195781996115")]
        public void ValidateCardNumber_WhenNumberIsValid_ReturnsTrue(string number)
        {
            Assert.True(LuhnValidator.IsValidCardNumber(number));
        }
    }
}
