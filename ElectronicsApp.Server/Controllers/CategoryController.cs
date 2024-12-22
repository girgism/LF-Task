using Application.app.Features.CategoryFeature;
using Application.app.Features.CategoryFeature.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ElectronicsApp.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpGet]
        [ProducesResponseType(typeof(List<GetCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<GetCategoryDto>), StatusCodes.Status400BadRequest)]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());
            return Ok(result.Value);
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetCategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(GetCategoryDto), StatusCodes.Status400BadRequest)]
        [Route("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            var result = await _mediator.Send(new GetCategoryByIdQuery
            {
                Id = categoryId
            });
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("AddNewCategory")]
        public async Task<IActionResult> AddNewCategory(AddCategoryDto newCategory)
        {
            var result = await _mediator.Send(new AddCategoryCommand
            {
                Name = newCategory.Name,
                Description = newCategory.Description
            });

            if (result.IsFailure)
            {
                return BadRequest(result.Value);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto category)
        {
            var result = await _mediator.Send(new UpdateCategoryCommand
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            });

            if (result.IsFailure)
            {
                return BadRequest(result.Value);
            }
            return Ok(result.Value);
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [Route("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await _mediator.Send(new DeleteCategoryCommand
            {
                Id = categoryId,

            });

            if (result.IsFailure)
            {
                return BadRequest(result.Value);
            }
            return Ok(result.Value);
        }
    }
}
