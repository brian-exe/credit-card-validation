using CreditCardValidation.Domain;
using System.Collections.Generic;

namespace CreditCardValidation.Abstractions.Repositories
{
    public interface ICardTypeRepository
    {
        IEnumerable<CardType> GetSupportedCardTypes();
    }
}
