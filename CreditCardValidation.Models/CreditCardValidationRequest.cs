using CreditCardValidation.Infrastructure.Logger.MaskingAttributes;
using System.Text.Json;

namespace CreditCardValidation.Models
{
    public class CreditCardValidationRequest
    {
        public long Number { get; set; }
        public int CVV { get; set; }

        [LogAsMasked(preserveLength:true)]
        public string Owner { get; set; }
        public CardExpirationModel Expiration { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }

    }
}
