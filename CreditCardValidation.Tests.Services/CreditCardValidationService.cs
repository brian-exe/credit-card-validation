using System;
using Xunit;

namespace CreditCardValidation.Tests.Services
{
    public class CreditCardValidationService
    {
        /// <summary>
        /// campos faltantes ==> error
        /// campos completos pero numero invalido, no se encontró tipo válido
        /// tipo reconocido pero invalido por luhn
        /// tipo reconocido pero largo inválido
        /// tipo reconocido pero cvv invalido
        /// tipo reconocido pero owner inválido
        /// tipo reconocido pero tarjeta expirada
        /// </summary>
        [Fact]
        public void ValidateCreditCard_WhenCalledWithNotAllFieldsProvided_ReturnsError()
        {

        }
    }
}
