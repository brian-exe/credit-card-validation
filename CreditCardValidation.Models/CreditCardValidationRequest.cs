namespace CreditCardValidation.Models
{
    public class CreditCardValidationRequest
    {
        public long Number { get; set; }
        public int CVV { get; set; }
        public string Owner { get; set; }
        public CardExpirationModel Expiration { get; set; }

    }
}
