using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace auction.Exceptions
{
    
    public class ProductBidAlreadyExpiredException : ApplicationException
    {
        public ProductBidAlreadyExpiredException() { }
        public ProductBidAlreadyExpiredException(string message) : base(message) { }
    }
    
}
