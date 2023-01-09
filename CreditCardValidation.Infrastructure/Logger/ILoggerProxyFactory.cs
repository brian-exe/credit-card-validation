namespace CreditCardValidation.Infrastructure.Logger
{
    public interface ILoggerProxyFactory
    {
        ILoggerProxy<TCategory> Create<TCategory>();
    }
}
