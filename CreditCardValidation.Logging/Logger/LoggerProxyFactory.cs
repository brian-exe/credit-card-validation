using Microsoft.Extensions.Logging;

namespace CreditCardValidation.Logging.Logger
{
    public class LoggerProxyFactory : ILoggerProxyFactory
    {
        private readonly ILoggerFactory loggerFactory;


        public LoggerProxyFactory(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }


        ILoggerProxy<TCategory> ILoggerProxyFactory.Create<TCategory>()
        {
            var logger = this.loggerFactory.CreateLogger<TCategory>();
            return new LoggerProxy<TCategory>(logger);
        }
    }
}
