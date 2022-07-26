using System;

namespace CreditCardValidation.Models
{
    public class CreditCardValidationRequest
    {
        public int Number { get; set; }
        public int CVC { get; set; }
        public string Owner { get; set; }
        public CardExpirationModel Expiration { get; set; }

    }
}
