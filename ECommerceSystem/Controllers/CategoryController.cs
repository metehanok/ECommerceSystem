using ECommerceSystem.Core.IServices;
using ECommerceSystem.Service.DTO;
using ECommerceSystem.Validations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ECommerceSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly CategoryCreateDTOValidator _categoryCreateValidator;
        private readonly CategoryUpdateDTOValidator _categoryUpdateValidator;
        public CategoryController(ICategoryService categoryService, CategoryCreateDTOValidator categoryCreateValidator, CategoryUpdateDTOValidator categoryUpdateValidator)
        {
            _categoryCreateValidator = categoryCreateValidator;
            _categoryUpdateValidator = categoryUpdateValidator;
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoriesById(int id)
        {
            var categories=await _categoryService.GetCategoriesByIdAsync(id);
            if (categories==null) return NotFound();
            return Ok(categories);
        }

        [HttpPost]
        public async Task <IActionResult> CreateCategories([FromBody] CategoryCreateDTO categoryCreateDto)
        {
            var validationResult = await _categoryCreateValidator.ValidateAsync(categoryCreateDto);
            if(!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e=>e.ErrorMessage));

            var createdCategory=await _categoryService.AddCategoriesAsync(categoryCreateDto);
            return CreatedAtAction(nameof(GetCategoriesById), new {id=createdCategory.Id},createdCategory);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id,[FromBody] CategoryUpdateDTO categoryUpdateDto)
        {
            var validationResult= await _categoryUpdateValidator.ValidateAsync(categoryUpdateDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            var updatedCategory = await _categoryService.UpdateCategoriesAsync(id,categoryUpdateDto);
            if (!updatedCategory) return NotFound();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
           var deletedCategory=await _categoryService.DeleteCategoriesAsync(id);
            if(!deletedCategory) return NotFound();
            return NoContent();
        }
    }
}
