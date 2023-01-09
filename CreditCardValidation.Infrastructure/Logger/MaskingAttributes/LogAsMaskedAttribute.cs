using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;

namespace CreditCardValidation.Infrastructure.Logger.MaskingAttributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class LogAsMaskedAttribute : Attribute, ILoggingMethodAttribute
    {
        readonly string _mask; 
        readonly bool _preserveLength; 
        public LogAsMaskedAttribute(bool preserveLength = false, string mask = "*") { 
            _mask= mask;
            _preserveLength= preserveLength;
        }

        private string GetValuePreservingLength(string value)
            => string.Concat(Enumerable.Repeat(_mask, value.Length));
        
        public bool TryGetValue(object value, out string result)
        {
            if (value == null)
            {
                result = "";
                return true;
            }

            if (value is string s)
            {
                result = _preserveLength ? GetValuePreservingLength(s) : _mask; 
                return true;
            }

            result = "";
            return false;
        }
    }
}
