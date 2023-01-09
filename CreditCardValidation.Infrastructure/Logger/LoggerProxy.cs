using Microsoft.Extensions.Logging;
using System.Reflection;
using CreditCardValidation.Infrastructure.Logger.MaskingRules;
using CreditCardValidation.Infrastructure.Logger.MaskingAttributes.Utils;
using CreditCardValidation.Infrastructure.Logger.MaskingAttributes;
using CreditCardValidation.Infrastructure.Logger.Processor;

namespace CreditCardValidation.Infrastructure.Logger
{
    public class LoggerProxy<TCategory> : ILoggerProxy<TCategory>
    {
        private readonly ILogProcessor logProcessor;

        public ILogger<TCategory> Logger { get; }

        public LoggerProxy(ILogger<TCategory> logger)
        {
            this.Logger = logger;
            this.logProcessor = new LogProcessor();
        }

        private void BasicLog(LogLevel logLevel, string message, params object[] args)
        {
            var processedLog = logProcessor.ProcessForLogging(message, args);
            this.Logger.Log(logLevel, processedLog);
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
