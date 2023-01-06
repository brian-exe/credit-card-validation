namespace CreditCardValidation.API.Logger
{
    public interface ILoggerProxyFactory
    {
        ILoggerProxy<TCategory> Create<TCategory>();
    }
}
