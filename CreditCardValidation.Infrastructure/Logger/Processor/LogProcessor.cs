using CreditCardValidation.Infrastructure.Logger.MaskingAttributes.Utils;
using CreditCardValidation.Infrastructure.Logger.MaskingAttributes;
using CreditCardValidation.Infrastructure.Logger.MaskingRules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace CreditCardValidation.Infrastructure.Logger.Processor
{
    public class LogProcessor
    {
        private readonly List<IMaskingRule> maskingRules = new();
        public LogProcessor()
        {
            maskingRules.Add(new CreditCardMaskingRule());
            maskingRules.Add(new IbanMaskingRule());
        }

        public string ProcessForLogging(string message, object[] args)
        {
            var result = ProcessArguments(message, args);
            result = ProcessGeneralMaskingRules(result);
            return result;
        }
        private string ProcessGeneralMaskingRules(string message)
        {
            var result = message;
            foreach (var maskingRule in maskingRules)
            {
                result = maskingRule.Mask(result);
            }
            return result;
        }

        private string ProcessArguments(string message, object[] args)
        {
            foreach (var arg in args)
            {
                ProcessArgument(arg);
            }
            return string.Format(message, args);
        }

        private void ProcessArgument(object arg)
        {
            Type type = arg.GetType();
            var properties = GetablePropertyFinder.GetProperties(type).ToList();
            if (properties.All(pi => pi.GetCustomAttribute<ILoggingMethodAttribute>() == null))
                return;

            var attributes = GetAttributesForProperties(properties);

            foreach (var property in properties)
            {
                var propValue = SafeGetPropValue(arg, property);

                if (attributes.TryGetValue(property, out var attribute))
                {
                    var result = "";
                    if (attribute.TryGetValue(propValue, out result))
                        property.SetValue(arg, result);
                }
            }
        }
        private Dictionary<PropertyInfo, ILoggingMethodAttribute> GetAttributesForProperties(List<PropertyInfo> properties) 
            =>  properties
                    .Select(pi => new { pi, Attribute = pi.GetCustomAttribute<ILoggingMethodAttribute>() })
                    .Where(o => o.Attribute != null)
                    .ToDictionary(o => o.pi, o => o.Attribute);

        private object SafeGetPropValue(object o, PropertyInfo pi)
        {
            try
            {
                return pi.GetValue(o);
            }
            catch (TargetInvocationException ex)
            {
                return $"The property accessor threw an exception: {ex.InnerException!.GetType().Name}";
            }
        }

    }
}
