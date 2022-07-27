using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreditCardValidation.Models
{
    public class GenericApplicationResponse<T> where T: class
    {
        public GenericApplicationResponse()
        {
            Errors = new List<ApplicationError>();
        }
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<ApplicationError> Errors { get; set; }
    }
}
