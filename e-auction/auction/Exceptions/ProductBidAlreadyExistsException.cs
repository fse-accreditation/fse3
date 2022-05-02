using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auction.Exceptions
{
    
    public class ProductBidAlreadyExistsException : ApplicationException
    {
        public ProductBidAlreadyExistsException() { }
        public ProductBidAlreadyExistsException(string message) : base(message) { }
    }
    
}
