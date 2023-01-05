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


        #region Properties

        public ILogger<TCategory> Logger { get; }

        #endregion


        #region Public Methods
        private void BasicLog(LogLevel logLevel, string message, params object[] args)
        {
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

        #region Log Actions


        #endregion


        #region Log Function


        #endregion


        #endregion


        #region Private Methods

        #endregion
    }
}
