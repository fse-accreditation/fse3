using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auction.Exceptions
{
   
    public class ProductConstraintsException : ApplicationException
    {
         public ProductConstraintsException() { }
        public ProductConstraintsException(string message) : base(message) { }
    }
}