using Microsoft.Extensions.Logging;

namespace CreditCardValidation.Infrastructure.Logger
{
    public interface ILoggerProxy<TCategory>
    {
        ILogger<TCategory> Logger { get; }

        void Log(string message);
        void LogInformation(string message, params object[] args);
        void LogError(string message, params object[] args);
        void LogDebug(string message, params object[] args);
        void LogWarning(string message, params object[] args);
    }
}
