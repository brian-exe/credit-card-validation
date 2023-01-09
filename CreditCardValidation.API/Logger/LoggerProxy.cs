using Microsoft.Extensions.Logging;
using System.Diagnostics.Eventing.Reader;

namespace CreditCardValidation.API.Logger
{
    public class LoggerProxy<TCategory> : ILoggerProxy<TCategory>
    {
        public LoggerProxy(ILogger<TCategory> logger)
        {
            this.Logger = logger;
        }

        public ILogger<TCategory> Logger { get; }

        private void BasicLog(LogLevel logLevel, string message, params object[] args)
        {
            //We can use this method to add other actions before and after logging
            this.Logger.Log(logLevel, message, args);
        }

        public void LogInformation(string message, params object[] args)
            => BasicLog(LogLevel.Information, message, args);

        public void LogError(string message, params object[] args)
            => BasicLog(LogLevel.Error, message, args);

        public void LogDebug(string message, params object[] args)
            => BasicLog(LogLevel.Debug, message, args);

        public void LogWarning(string message, params object[] args)
            =>BasicLog(LogLevel.Warning, message, args);

        public void Log(string message)
            =>LogInformation(message, null);
    }
}
