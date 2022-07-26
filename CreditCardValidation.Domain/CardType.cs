using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Domain
{
    public class CardType
    {
        public string Name { get; set; }
        public List<NumberPrefixRule> PrefixRules { get; set; }
        public List<NumberLengthRule> LengthRules { get; set; }
        public List<CVVLengthRule> CVVLengthRules { get; set; }
        public bool LuhnValidation { get; set; }
    }
}
