using Microsoft.Extensions.Logging;
using System.Diagnostics.Eventing.Reader;
using CreditCardValidation.Logging.Enum;
using Serilog.Events;
using Serilog.Context;

namespace CreditCardValidation.Logging.Logger
{
    public class LoggerProxy<TCategory> : ILoggerProxy<TCategory>
    {
        public LoggerProxy(ILogger<TCategory> logger)
        {
            this.Logger = logger;
        }
        private int loggingMaskingBehavior = LoggingMaskingBehavior.None;

        public ILogger<TCategory> Logger { get; }

        private void BasicLog(LogLevel logLevel, string message, params object[] args)
        {
            LogContext.PushProperty(nameof(LoggingMaskingBehavior), loggingMaskingBehavior);
            //We can use this method to add other actions before and after logging
            this.Logger.Log(logLevel, message, args.Append(loggingMaskingBehavior));
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

        public ILoggerProxy<TCategory> IgnoringMaskingOperators()
        {
            loggingMaskingBehavior = LoggingMaskingBehavior.Ignore;
            return this;
        }

        public ILoggerProxy<TCategory> StopMaskingOnFirstMaskOperatorMatch()
        {
            loggingMaskingBehavior = LoggingMaskingBehavior.StopOnFirst;
            return this;
        }
    }
}
