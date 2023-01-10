using Serilog.Enrichers.Sensitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Logging.LoggerCustomizations
{
    public class CustomIbanMaskingOperator : RegexMaskingOperator
    {
        private const string IbanReplacePattern = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";

        public CustomIbanMaskingOperator()
            : base("[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}")
        {
        }
    }
}
