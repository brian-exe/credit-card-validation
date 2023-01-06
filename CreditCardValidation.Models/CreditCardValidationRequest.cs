using Collector.Serilog.Enrichers.SensitiveInformation.Attributed;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using Destructurama.Attributed;

namespace CreditCardValidation.Models
{
    public class CreditCardValidationRequest
    {
        //[Destructurama.Attributed.LogAsScalar(isMutable:true)]
        [LogMasked(PreserveLength =true, ShowLast = 4)]
        public string Number { get; set; }

        //[LogAsSensitive]
        public int CVV { get; set; }
        public string Owner { get; set; }
        public CardExpirationModel Expiration { get; set; }

        //public override string ToString()
        //{
        //    return JsonSerializer.Serialize(this);
        //}
    }
}

//https://github.com/destructurama/attributed
//https://github.com/collector-bank/serilog-enrichers-sensitiveinformation