using ThePlannerAPI.DTOs.Category;

namespace ThePlannerAPI.Interface
{
    public interface ICategory
    {
        Task<GetCategoryDTO> GetCategory(int categoryId);
        Task<List<GetCategoryDTO>> GetCategories();
        Task<int> AddCategory(CategoryDTO addCategoryDTO);
        Task<int> UpdateCategory(CategoryDTO category, int categoryId);
        Task<int> DeleteCategory(int categoryId);
    }
}
