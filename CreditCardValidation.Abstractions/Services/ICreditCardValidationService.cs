using CreditCardValidation.Models;
using System;

namespace CreditCardValidation.Abstractions.Services
{
    public interface ICreditCardValidationService
    {
        GenericApplicationResponse<CreditCardValidationResponse> ValidateCreditCard(CreditCardValidationRequest model);
    }
}
