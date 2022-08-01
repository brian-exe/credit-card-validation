using CreditCardValidation.Abstractions.Repositories;
using CreditCardValidation.Domain;
using System.Collections.Generic;

namespace CreditCardValidation.Repositories
{
    public class CardTypeRepository : ICardTypeRepository
    {
        public IEnumerable<CardType> GetSupportedCardTypes()
        {
            var response = new List<CardType>();

            response.Add(new CardType()
            {
                Name = "Visa",
                PrefixRules = new List<NumberPrefixRule>() { new NumberPrefixRule() { Prefix = 4 } },
                LengthRules = new List<NumberLengthRule>() { new NumberLengthRule() { Length = 16 }, new NumberLengthRule() { Length = 13 } },
                CVVLengthRules = new List<CVVLengthRule>() { new CVVLengthRule() { Length = 3 } },
                LuhnValidation = true,
            });

            response.Add(new CardType()
            {
                Name = "Mastercard",
                PrefixRules = new List<NumberPrefixRule>() {
                    new NumberPrefixRule() { Prefix = 5},
                    new NumberPrefixRule() { Prefix = 2221 },
                    new NumberPrefixRule() { Prefix = 2720 }
                },
                LengthRules = new List<NumberLengthRule>() { new NumberLengthRule() { Length = 16 } },
                CVVLengthRules = new List<CVVLengthRule>() { new CVVLengthRule() { Length = 3 } },
                LuhnValidation = true,
            });

            response.Add(new CardType()
            {
                Name = "American Express",
                PrefixRules = new List<NumberPrefixRule>() { new NumberPrefixRule() { Prefix = 34 }, new NumberPrefixRule() { Prefix = 37 } },
                LengthRules = new List<NumberLengthRule>() { new NumberLengthRule() { Length = 15 } },
                CVVLengthRules = new List<CVVLengthRule>() { new CVVLengthRule() { Length = 4 } },
                LuhnValidation = true,
            });

            return response;
        }
    }
}
