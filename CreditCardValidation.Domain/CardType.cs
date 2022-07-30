using System.Collections.Generic;

namespace CreditCardValidation.Domain
{
    public class CardType
    {
        public CardType()
        {
            PrefixRules = new List<NumberPrefixRule>();
            LengthRules = new List<NumberLengthRule>();
            CVVLengthRules = new List<CVVLengthRule>();
            LuhnValidation = true;
        }
        public string Name { get; set; }
        public List<NumberPrefixRule> PrefixRules { get; set; }
        public List<NumberLengthRule> LengthRules { get; set; }
        public List<CVVLengthRule> CVVLengthRules { get; set; }
        public bool LuhnValidation { get; set; }
    }
}
