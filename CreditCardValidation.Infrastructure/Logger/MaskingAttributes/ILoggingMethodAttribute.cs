namespace CreditCardValidation.Infrastructure.Logger.MaskingAttributes
{
    public interface ILoggingMethodAttribute
    {
        bool TryGetValue(object value, out string result);
    }
}
