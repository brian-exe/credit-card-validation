using CreditCardValidation.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Abstractions.Repositories
{
    public interface ICardTypeRepository
    {
        IEnumerable<CardType> GetSupportedCardTypes();
    }
}
