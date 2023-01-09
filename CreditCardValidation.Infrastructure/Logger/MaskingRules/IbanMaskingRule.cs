using Microsoft.VisualBasic;
using System.Text.RegularExpressions;

namespace CreditCardValidation.Infrastructure.Logger.MaskingRules
{
    public class IbanMaskingRule : IMaskingRule
    {
        //private string Pattern = "[a-zA-Z]{2}[0-9]{2}[a-zA-Z0-9]{4}[0-9]{7}([a-zA-Z0-9]?){0,16}";
        private string Pattern = "^[A-Z]{2}\\d{2} (?:\\d{4} ){3}\\d{4}(?: \\d\\d?)?$";

        public string Mask(string tobeMasked)
        {
            var mask = new string('*', tobeMasked.Length);
            var _regex = new Regex(Pattern);

            string text = _regex.Replace(tobeMasked, mask);

            return text;
        }
    }
}
