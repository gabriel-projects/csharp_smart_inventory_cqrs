using Api.GRRInnovations.SmartInventory.Domain.Entities;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.In;
using Api.GRRInnovations.SmartInventory.Domain.Wrappers.Out;
using Api.GRRInnovations.SmartInventory.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.GRRInnovations.SmartInventory.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] WrapperInCategory<CategoryModel> dto)
        {
            var result = await dto.Result().ConfigureAwait(false);

            var category = await _categoryService.CreateAsync(result).ConfigureAwait(false);

            var response = await WrapperOutCategory.From(category).ConfigureAwait(false);
            return new OkObjectResult(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var categories = await _categoryService.GetAllAsync().ConfigureAwait(false);
            return new OkObjectResult(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var category = await _categoryService.GetByIdAsync(id).ConfigureAwait(false);
            return new OkObjectResult(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] WrapperInCategory<CategoryModel> dto)
        {
            var result = await dto.Result().ConfigureAwait(false);

            await _categoryService.UpdateAsync(id, result).ConfigureAwait(false);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _categoryService.DeleteAsync(id).ConfigureAwait(false);
            return NoContent();
        }
    }
}
