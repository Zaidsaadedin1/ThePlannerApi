using Microsoft.EntityFrameworkCore;
using ThePlannerAPI.Context;

namespace ThePlannerAPI.Shared
{
    public class UserValidationQuery
    {
        public ApplicationDbContext _context;
        public UserValidationQuery(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsUserUsernameExist(string username)
        {
            return await _context.Users.AnyAsync(u => u.Username == username);
        }
        public async Task<bool> IsUserExist(string id)
        {
            return await _context.Users.AnyAsync(u => u.Id == id);
        }
    }
}
