using System;
using System.Linq;

namespace CreditCardValidation.Services.Common
{
    public static class LuhnValidator
    {
        public static bool IsValidCardNumber(string cardNumber)
        {
            cardNumber.Trim().Replace(" ", "");

            if (!cardNumber.All(x => Char.IsDigit(x)))
                return false;

            int length = cardNumber.Length;

            if (length == 0 || length == 1)
                return false;

            int nSum = 0;

            // arrays start from 0 so we always have to subtract 1.
            // If number of digits is Even, we should double odd positions. 
            // If number of digits is Odd, we should double even positions. 
            // Because we want to exclude last digit we then subsract always 2 or 3.
            //int startingIndex = (length % 2 == 0) ? length - 2 : length - 3; 
            int startingIndex = length - 2;

            bool shouldDoubleDigit = true;
            for (int i = startingIndex; i >= 0; i--)
            {

                int d = cardNumber[i] - '0'; //ascii substraction

                if (shouldDoubleDigit)
                {
                    d *= 2;
                    d -= (d > 9) ? 9 : 0;
                }

                nSum += d;

                shouldDoubleDigit = !shouldDoubleDigit;
            }
            int checkDigit = cardNumber[length - 1] - '0';
            return ((nSum + checkDigit) % 10 == 0);
        }
    }
}
