using System.ComponentModel.DataAnnotations;
using System;
using auction.Validator;
namespace auction.Models
{

    public class ProductDetails
    {

        [Required(ErrorMessage = "ProductId is required")]
        public string ProductId { get; set; }
        [Required(ErrorMessage = "ProductName is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "The {0} value must be between {2} to {1} characters.")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Short Description is required")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "Detailed Description is required")]
        public string DetailedDescription { get; set; }
        [Required(ErrorMessage = "StartingPrice is required")]
        [RegularExpression(@"^[0-9]*$",
        ErrorMessage = "Starting price should be number")]
        public decimal StartingPrice { get; set; }
        [Required(ErrorMessage = "BidEndDate is required")]
        [FutureDate]
        public DateTime BidEndDate { get; set; }
        [Required(ErrorMessage = "SellerEmail is required")]
        [RegularExpression(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$",
        ErrorMessage = "Seller email format is invalid")]
        public string SellerEmail { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [CategoryNotListed]
        public string Category { get; set; }



    }

}
