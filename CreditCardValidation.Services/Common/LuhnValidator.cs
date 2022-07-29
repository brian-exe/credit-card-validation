using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Services.Common
{
    public static class LuhnValidator
    {
        public static bool IsValidCardNumber(string cardNumber)
        {
			int nDigits = cardNumber.Length;

			if (nDigits == 0 || nDigits == 1)
				return false;

			int nSum = 0;
			bool shouldDoubleDigit = true;
			for (int i = nDigits - 1; i > 0; i--)
			{

				int d = cardNumber[i-1] - '0'; //ascii substraction

				if (shouldDoubleDigit)
                {
					d *= 2;
					d -= (d > 9) ? 9 : 0;
                }

				nSum += d;

				shouldDoubleDigit = !shouldDoubleDigit;
			}
			int checkDigit = cardNumber[nDigits - 1] - '0';
			return ((nSum + checkDigit) % 10 == 0);
		}
    }
}
