using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Models
{
    public class ApplicationError
    {
        public string Type { get; set; }
        public int Code { get; set; }
        public string Message { get; set; }
    }
}
