using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Logging.Enum
{
    public static class LoggingMaskingBehavior
    {
        public static readonly int None = 0;
        public static readonly int Ignore = 1;
        public static readonly int StopOnFirst = 2;
    }
}
