using CreditCardValidation.Models;

namespace CreditCardValidation.Abstractions.Services
{
    public interface ICreditCardValidationService
    {
        GenericApplicationResponse<CreditCardValidationResponse> ValidateCreditCard(CreditCardValidationRequest model);
    }
}
