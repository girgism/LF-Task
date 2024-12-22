using Application.app.Features.ProductFeature;
using Application.app.Features.ProductFeature.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<GetProductDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<GetProductDto>), StatusCodes.Status400BadRequest)]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("AddNewProduct")]
        public async Task<IActionResult> AddNewProduct(AddNewProductDto newProduct)
        {
            var result = await _mediator.Send(new AddNewProductCommand
            {
                Name = newProduct.Name,
                Description = newProduct.Description,
                Price = newProduct.Price,
                CategoryId = newProduct.CategoryId
            });

            if (result.IsFailure)
            {
                return BadRequest(result.Value);
            }
            return Ok(result.Value);
        }
    }
}
