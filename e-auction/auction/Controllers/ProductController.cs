
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using auction.Entity;
using auction.Exceptions;
using auction.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using auction.Commands;
using auction.Queries;
using auction.Services;

namespace auction.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<ProductController> _logger;
        public ProductController(ILogger<ProductController> logger, IMediator mediator
         )
        {
            _logger = logger;
            _mediator = mediator;

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductDetails productDetail)
        {
            try
            {

                var seller = await _mediator.Send(new SellerDetailSearchByEmailQuery()
                {
                    SellerEmail=productDetail.SellerEmail

                });

                if (seller == null)
                {
                    throw new SellerNotFoundException($"Seller with {productDetail.SellerEmail} not registered!!");

                }

                Product product = MapToEntity(productDetail, seller);
                AddProductCommand command = new AddProductCommand
                {
                    Product = product
                };
                await _mediator.Send(command);
                return CreatedAtAction(nameof(Post), productDetail.ProductId);
            }
            catch (SellerNotFoundException ex)
            {
                _logger.LogInformation($"Required product does not exist");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Adding the stock: {ex.Message}");
                return StatusCode(500);
            }
        }

        private Product MapToEntity(ProductDetails productDetail, UserDetail userDetail)
        {
            return new Product
            {
                ProductId = productDetail.ProductId,
                ProductName = productDetail.ProductName,
                ShortDescription = productDetail.ShortDescription,
                DetailedDescription = productDetail.DetailedDescription,
                StartingPrice = productDetail.StartingPrice,
                BidEndDate = productDetail.BidEndDate,
                UserDetail = userDetail

            };

        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete]
        [Route("{productId}")]
        public async Task<IActionResult> Delete(string productId)
        {
            try
            {
                var product = await _mediator.Send(new ProductSearchByProductIdQuery()
                {
                    ProductId = productId
                });

                if (product == null)
                {
                    throw new ProductNotFoundException("Product id not found");
                }

                var productBidList = await _mediator.Send(new ProductBidSearchByProductIdQuery()
                {
                    ProductId = productId
                });
                if ((productBidList != null  && productBidList.Any())|| product.BidEndDate <= DateTime.Now)
                {
                    throw new ProductConstraintsException("Product can't deleted. Either at least one bid is placed on that product or product is expired");
                }

                DeleteProductCommand command = new DeleteProductCommand
                {
                    ProductId = productId
                };
                await _mediator.Send(command);

                return Ok();
            }
            catch (ProductNotFoundException pnf)
            {
                _logger.LogInformation($"Required product does not exist");
                return NotFound(pnf.Message);
            }
            catch (ProductConstraintsException ex)
            {
                _logger.LogInformation($"product can't be deleted");
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in fetching data: {ex.Message}");
                return StatusCode(500);
            }
        }
    }
}
