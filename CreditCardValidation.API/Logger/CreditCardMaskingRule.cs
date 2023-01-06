using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditCardValidation.API.Logger
{
    public class CreditCardMaskingRule : IMaskingRule
    {
        //Extract in groups
        //  sep => separator between group of digits
        //  trailing4 => last 4 digits
        //this groups are used later to format string and replacing them in the correct place
        private const string CreditCardPartialReplacePattern = "(?<leading4>\\d{4}(?<sep>[ -]?)\\d{4}\\k<sep>?\\d{4}\\k<sep>?)(?<trailing4>\\d{4})";
        private readonly string _replacementPattern = "{0}${{sep}}{0}${{sep}}{0}${{sep}}${{trailing4}}";
        public CreditCardMaskingRule()
        { }

        public string Mask(string tobeMasked)
        {
            var mask4digits = new string('*', 4);
            var formatted = string.Format(_replacementPattern, mask4digits);
            var _regex = new Regex(CreditCardPartialReplacePattern);

            string text = _regex.Replace(tobeMasked, (Match match) => match.Result(formatted));

            return text;
        }
    }
}
