using Serilog.Enrichers.Sensitive;
using System.Text.RegularExpressions;

namespace CreditCardValidation.Logging
{
    public class CustomCCMaskingOperator : RegexMaskingOperator
    {
        private const string CreditCardPartialReplacePattern = "(?<leading4>\\d{4}(?<sep>[ -]?)\\d{4}\\k<sep>?\\d{4}\\k<sep>?)(?<trailing4>\\d{4})";

        private const string CreditCardFullReplacePattern = "(?<toMask>\\d{4}(?<sep>[ -]?)\\d{4}\\k<sep>*\\d{4}\\k<sep>*\\d{4})";

        private readonly string _replacementPattern;

        public CustomCCMaskingOperator()
            : this(fullMask: true)
        {
        }

        public CustomCCMaskingOperator(bool fullMask)
            : base(fullMask ? CreditCardFullReplacePattern : CreditCardPartialReplacePattern, RegexOptions.IgnoreCase | RegexOptions.Compiled)
        {
            _replacementPattern = (fullMask ? "{0}${{sep}}{0}${{sep}}{0}${{sep}}{0}" : "{0}${{sep}}{0}${{sep}}{0}${{sep}}${{trailing4}}");
        }

        protected override string PreprocessMask(string mask)
        {
            var mask4digits = new string(mask[0], 4);
            var formatted = string.Format(_replacementPattern, mask4digits);
            return formatted;
        }
    }
}
