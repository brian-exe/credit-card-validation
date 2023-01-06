namespace CreditCardValidation.API.Logger
{
    public interface IMaskingRule
    {
        string Mask(string tobeMasked);
    }
}