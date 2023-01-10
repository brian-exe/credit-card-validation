using Microsoft.Extensions.Logging;

namespace CreditCardValidation.Logging.Logger
{
    public interface ILoggerProxy<TCategory>
    {
        ILogger<TCategory> Logger { get; }

        ILoggerProxy<TCategory> IgnoringMaskingOperators();
        ILoggerProxy<TCategory> StopMaskingOnFirstMaskOperatorMatch();
        void Log(string message);
        void LogInformation(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
