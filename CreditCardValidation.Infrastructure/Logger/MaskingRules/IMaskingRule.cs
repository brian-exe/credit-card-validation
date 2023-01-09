namespace CreditCardValidation.Infrastructure.Logger.MaskingRules
{
    public interface IMaskingRule
    {
        string Mask(string tobeMasked);
    }
}