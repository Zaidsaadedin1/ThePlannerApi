using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ThePlannerAPI.Context;
using ThePlannerAPI.DTOs.Category;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;

namespace ThePlannerAPI.Services
{
    public class CategoryService : ICategory
    {
        public readonly ApplicationDbContext context;
        public CategoryService(ApplicationDbContext context)
        {
            this.context = context;

        }

        public async Task<int> AddCategory(CategoryDTO addCategoryDTO)
        {
            var existCategory = await context.Categories.FirstOrDefaultAsync(c => c.CategoryName == addCategoryDTO.CategoryName);

            var newCategory = new Category
            {
                CategoryName = addCategoryDTO.CategoryName,
            };

            await context.Categories.AddAsync(newCategory);
            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Created;
        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            var existCategory = await context.Categories.FindAsync(categoryId);

            context.Categories.RemoveRange(existCategory!);
            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Deleted;

        }

        public async Task<List<GetCategoryDTO>> GetCategories()
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesList = categories.Select(category => new GetCategoryDTO
            {
                Id = category.Id,
                CategoryName = category.CategoryName,
            }).ToList();
            return categoriesList;
        }

        public async Task<GetCategoryDTO> GetCategory(int categoryId)
        {
            var existCategory = await context.Categories.FindAsync(categoryId);
            var getCategory = new GetCategoryDTO
            {
                Id = existCategory!.Id,
                CategoryName = existCategory.CategoryName,
            };
            return getCategory;
        }

        public async Task<int> UpdateCategory(CategoryDTO category, int categoryId)
        {
            var existCategory = await context.Categories.FindAsync(categoryId);

            existCategory!.CategoryName = category.CategoryName;
            await context.SaveChangesAsync();

            return (int)ApiResponseStatus.Updated;
        }
    }
}
