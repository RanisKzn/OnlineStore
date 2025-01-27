using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Services;

namespace OnlineStore.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IGenericService<Category> _genericService;

        public CategoriesController(IGenericService<Category> genericService)
        {
            _genericService = genericService;
        }

        [HttpGet]
        public async Task<JsonResult> GetCategories()
        {
            var categories = await _genericService.GetAllAsync();
            return new JsonResult(categories);
        }

        [HttpGet("{id}")]
        public async Task<JsonResult> GetCategory(Guid id)
        {
            var category = await _genericService.GetByIdAsync(id);
            return new JsonResult(category);
        }

        [HttpPost]
        public async Task<JsonResult> PostCategory(Category category)
        {
            await _genericService.AddAsync(category);
            return new JsonResult(CreatedAtAction(nameof(GetCategory), new { id = category.Id }, category));
        }

        [HttpPut("{id}")]
        public async Task<JsonResult> PutCategory (Guid id, Category category)
        {
            if (id != category.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.UpdateAsync(category);
            return new JsonResult(NoContent());
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> DeleteCategory(Guid id)
        {
            var category = await _genericService.GetByIdAsync(id);
            if (id != category.Id)
            {
                return new JsonResult(BadRequest());
            }
            await _genericService.DeleteAsync(id);
            return new JsonResult(NoContent());
        }
    }
}
