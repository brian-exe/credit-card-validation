using CreditCardValidation.Abstractions.Services;
using CreditCardValidation.Models;
using System;

namespace CreditCardValidation.Services
{
    public class CreditCardValidationService : ICreditCardValidationService
    {
        public GenericApplicationResponse<CreditCardValidationResponse> ValidateCreditCard(CreditCardValidationRequest model)
        {
            throw new NotImplementedException();
        }
    }
}
