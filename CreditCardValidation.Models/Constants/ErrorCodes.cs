namespace CreditCardValidation.Models.Constants
{
    public static class ErrorCodes
    {
        public const int MissingFields = 100;
        public const int UnrecognizedCardType = 101;
        public const int InvalidCardNumber = 102;
        public const int InvalidCardNumberLength = 103;
        public const int InvalidCardCVVLength = 104;
        public const int InvalidOwnerData = 105;
        public const int ExpiredCard = 106;

    }
}
