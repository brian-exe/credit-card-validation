using Serilog.Enrichers.Sensitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CreditCardValidation.Logging.LoggerCustomizations
{
    public abstract class CustomRegexMaskingOperator : IMaskingOperator
    {
        private readonly Regex _regex;
        public int Precedence { get; set; }

        protected CustomRegexMaskingOperator(string regexString)
            : this(regexString, RegexOptions.Compiled)
        {
        }

        protected CustomRegexMaskingOperator(string regexString, RegexOptions options)
        {
            _regex = new Regex(regexString ?? throw new ArgumentNullException("regexString"), options);
            if (string.IsNullOrWhiteSpace(regexString))
            {
                throw new ArgumentOutOfRangeException("regexString", "Regex pattern cannot be empty or whitespace.");
            }
        }

        public MaskingResult Mask(string input, string mask)
        {
            string mask2 = mask;
            string input2 = PreprocessInput(input);
            if (!ShouldMaskInput(input2))
            {
                return MaskingResult.NoMatch;
            }

            string text = _regex.Replace(input2, (Match match) => ShouldMaskMatch(match) ? match.Result(PreprocessMask(mask2)) : match.Value);
            MaskingResult result = default(MaskingResult);
            result.Result = text;
            result.Match = text != input;
            return result;
        }

        protected virtual bool ShouldMaskInput(string input)
        {
            return true;
        }

        protected virtual string PreprocessInput(string input)
        {
            return input;
        }

        protected virtual string PreprocessMask(string mask)
        {
            return mask;
        }

        protected virtual bool ShouldMaskMatch(Match match)
        {
            return true;
        }
    }
}
