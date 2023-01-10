
namespace CreditCardValidation.Logging.Logger
{
    public interface ILoggerProxyFactory
    {
        ILoggerProxy<TCategory> Create<TCategory>();
    }
}
