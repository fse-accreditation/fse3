using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auction.Exceptions
{
    
    public class ProductBidNotExistsException : ApplicationException
    {
        public ProductBidNotExistsException() { }
        public ProductBidNotExistsException(string message) : base(message) { }
    }
    
}
