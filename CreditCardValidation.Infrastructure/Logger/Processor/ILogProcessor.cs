namespace CreditCardValidation.Infrastructure.Logger.Processor
{
    public interface ILogProcessor
    {
        string ProcessForLogging(string message, object[] args);
    }
}