using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog.Sinks.Graylog;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog.Enrichers.Sensitive;
using CreditCardValidation.Models;
using CreditCardValidation.Logging.LoggerCustomizations;
using CreditCardValidation.Logging.Logger;
using Serilog;
using Serilog.Extensions.Logging;

public class Program
{
    public static void Main(string[] args)
    {
        //setup our DI
        var serviceProvider = new ServiceCollection();

        Serilog.Debugging.SelfLog.Enable(Console.Error);
        var serilog = new LoggerConfiguration()
            .MinimumLevel.Error()
            .Enrich.WithProcessId()
            .Enrich.WithThreadId()
            .Enrich.WithEnvironmentUserName()
            .Enrich.WithMachineName()
            .Enrich.FromLogContext()
            .Enrich.With(new CustomSensitiveDataEnricher(options =>
            {
                options.MaskingOperators = new List<IMaskingOperator>()
                {
                        new CustomCCMaskingOperator(fullMask: false),
                        new CustomIbanMaskingOperator(),
                        new CustomEmailMaskingOperator(),
                };
                options.MaskValue = "*";
                options.MaskProperties.Add("CVV");
                options.ExcludeProperties.Add("LoggingMaskingBehavior");

            }))
            .WriteTo.Graylog(new GraylogSinkOptions
            {
                HostnameOrAddress = "localhost",
                Port = 12201,
            })
            .WriteTo.Console(theme: AnsiConsoleTheme.Code)
            .WriteTo.File(path: "c:\\Temp\\serilog.log")
            .CreateLogger();

        ILoggerFactory loggerFactory = new LoggerFactory().AddSerilog(serilog);
        serviceProvider.AddSingleton(loggerFactory);
        serviceProvider.AddSingleton<ILoggerProxyFactory, LoggerProxyFactory>();


        Execute(serviceProvider.BuildServiceProvider());
    }

    private static void Execute(ServiceProvider provider)
    {
        var loggerProxy = provider.GetService<ILoggerProxyFactory>().Create<Program>();

        var req = new CreditCardValidationRequest
        {
            CVV = 123,
            Number = "4111000022223333",
            Expiration = new CardExpirationModel { Month = 10, Year = 2022 },
            Owner = "John Doe"
        };

        var iban = "EE10A23511111114234567894234234";
        loggerProxy.LogError("Error ocurred. req is " + req.Number);
        loggerProxy.LogError("the CVV is {0}", req.CVV);
        loggerProxy.LogError("Request received {0}", req);
        loggerProxy.LogError("{@request_}", req);
        loggerProxy.LogError("The iban is {IBAN}", "EE10A23511111114234567894234234");
        loggerProxy.LogError("The iban is " + iban);
    }
}