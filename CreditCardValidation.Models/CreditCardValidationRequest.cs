using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace CreditCardValidation.Models
{
    public class CreditCardValidationRequest
    {
        public long Number { get; set; }
        public int CVV { get; set; }
        public string Owner { get; set; }
        public CardExpirationModel Expiration { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
