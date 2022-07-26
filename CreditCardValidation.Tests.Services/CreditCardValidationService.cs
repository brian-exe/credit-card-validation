using System;
using Xunit;

namespace CreditCardValidation.Tests.Services
{
    public class CreditCardValidationService
    {
        /// <summary>
        /// campos faltantes ==> error
        /// campos completos pero numero invalido, no se encontr� tipo v�lido
        /// tipo reconocido pero invalido por luhn
        /// tipo reconocido pero largo inv�lido
        /// tipo reconocido pero cvv invalido
        /// tipo reconocido pero owner inv�lido
        /// tipo reconocido pero tarjeta expirada
        /// </summary>
        [Fact]
        public void ValidateCreditCard_WhenCalledWithNotAllFieldsProvided_ReturnsError()
        {

        }
    }
}
