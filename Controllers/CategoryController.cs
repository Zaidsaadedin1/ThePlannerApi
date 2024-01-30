
using ThePlannerAPI.DTOs.Category;
using ThePlannerAPI.Interface;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using FluentValidation;
using ThePlannerAPI.Shared;
using ThePlannerAPI.Model;
using ThePlannerAPI.Validators.Assignment;
using FluentValidation.AspNetCore;

namespace ThePlannerAPI.Controllers
{

    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryService;
        public readonly IValidator<CategoryDTO> _categoryValidator;
        public readonly CategoryValidationQuery _categoryValidationQuery;
        public CategoryController(
            ICategory categoryService,
            IValidator<CategoryDTO> categoryValidator,
            CategoryValidationQuery categoryValidationQuery)
        {
            _categoryService = categoryService;
            _categoryValidator = categoryValidator;
            _categoryValidationQuery = categoryValidationQuery;

        }

        [HttpPost("/Category")]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDTO category)
        {
            var validationResult = await _categoryValidator.ValidateAsync(category);

            if (!validationResult.IsValid)
            {
                validationResult.AddToModelState(this.ModelState);
                return BadRequest(validationResult);
            }


            var isCategoryExist = await _categoryValidationQuery.IsCategoryExist(category.CategoryName);
            if (isCategoryExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With Name: ({category.CategoryName}),Dose Already Exist"
                });
            }

            var result = await _categoryService.AddCategory(category);

            return Ok(new { Message = $"Category Added Successfully  With Name: ({category.CategoryName})" });
            

        }

        [HttpGet("/Categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null)
            {
                return NotFound(new { Message = "There Is No Categories Found" });
            }
            return Ok(categories);

        }

        [HttpGet("/Category")]
        public async Task<IActionResult> GetCategory([FromQuery] int categoryId)
        {

            var isCategoryExist = await _categoryValidationQuery.IsCategoryExist(categoryId);
            if (!isCategoryExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With ID: ({categoryId}),Dose Not Exist"
                });
            }

            var category = await _categoryService.GetCategory(categoryId);

            return Ok(category);
        }

        [HttpPut("/Category")]
        public async Task<IActionResult> UpdateCategory([FromQuery] int categoryId, [FromBody] CategoryDTO category)
        {
            var isCategoryExist = await _categoryValidationQuery.IsCategoryExist(categoryId);
            if (!isCategoryExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With ID: ({categoryId}),Dose Not Exist"
                });
            }
            var isCategoryNameExist = await _categoryValidationQuery.IsCategoryExist(category.CategoryName);
            if (isCategoryNameExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With Name: ({category.CategoryName}),Dose Already Exist"
                });
            }

            var result = await _categoryService.UpdateCategory(category, categoryId);

            return Ok(new { Message = "Category updated successfully" });
        }

        [HttpDelete("/Category")]
        public async Task<IActionResult> DeleteCategory([FromQuery] int categoryId)
        {

            var isCategoryExist = await _categoryValidationQuery.IsCategoryExist(categoryId);
            if (!isCategoryExist)
            {
                return BadRequest(new
                {
                    Message = $"Category With ID: ({categoryId}),Dose Not Exist"
                });
            }
         
            var result = await _categoryService.DeleteCategory(categoryId);

            return Ok(new { Message = $"Category deleted successfully With ID: ({categoryId})" });


        }





    }
}
