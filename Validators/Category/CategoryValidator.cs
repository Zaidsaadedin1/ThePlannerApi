using FluentValidation;
using ThePlannerAPI.DTOs.Category;
using ThePlannerAPI.DTOs.Task;

namespace ThePlannerAPI.Validators.Category
{
    public class CategoryValidator : AbstractValidator<CategoryDTO>
    {
        public CategoryValidator()
        {
            RuleFor(c=> c.CategoryName).NotEmpty().WithMessage("Category Name Must Be Not Empty");
           
        }
    }
}
