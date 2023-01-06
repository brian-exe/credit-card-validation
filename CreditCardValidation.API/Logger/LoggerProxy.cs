using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Diagnostics.Eventing.Reader;
using System;
using System.Collections.Generic;

namespace CreditCardValidation.API.Logger
{
    public class LoggerProxy<TCategory> : ILoggerProxy<TCategory>
    {
        private List<IMaskingRule> maskingRules = new();
        public LoggerProxy(ILogger<TCategory> logger)
        {
            this.Logger = logger;
            maskingRules.Add(new CreditCardMaskingRule());
        }


        #region Properties

        public ILogger<TCategory> Logger { get; }

        #endregion


        #region Public Methods
        private void BasicLog(LogLevel logLevel, string message, params object[] args)
        {
            var processedLog = PreProcessLog(message, args);
            this.Logger.Log(logLevel, processedLog);
        }

        private string PreProcessLog(string message, object[] args)
        {
            var result = ProcessPropertiesForLog(message, args);
            result = ProcessGeneralMaskingRules(result);
            return result;
        }

        private string ProcessGeneralMaskingRules(string message)
        {
            var result = message;
            foreach(var maskingRule in maskingRules)
            {
                result = maskingRule.Mask(result);
            }
            return result;
        }

        private string ProcessPropertiesForLog(string message, object[] args)
        {
            return string.Format(message, args);
            var newArgs = new object[args.Length];
            foreach(var arg in args)
            {
                Type myType = arg.GetType();
                MemberInfo[] myMembers = myType.GetMembers();

                for (int i = 0; i < myMembers.Length; i++)
                {
                    Object[] myAttributes = myMembers[i].GetCustomAttributes(true);
                    if (myAttributes.Length > 0)
                    {
                        for (int j = 0; j < myAttributes.Length; j++)
                            Console.WriteLine("The type of the attribute is {0}.", myAttributes[j]);
                    }
                }
            }
            return ";";
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
