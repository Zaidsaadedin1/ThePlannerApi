using Microsoft.EntityFrameworkCore;
using ThePlannerAPI.Context;

namespace ThePlannerAPI.Shared
{
    public class CategoryValidationQuery
    {
        public ApplicationDbContext _context;
        public CategoryValidationQuery(ApplicationDbContext context)
        {
            _context = context;
        }
                
        public async Task<bool> IsCategoryExist(string name)
        {
            return await _context.Categories.AnyAsync(c => c.CategoryName == name);
        }
        public async Task<bool> IsCategoryExist(int id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }
    }
}
