using Microsoft.EntityFrameworkCore;
using System;
using ThePlannerAPI.Context;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.Task;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Model;

namespace ThePlannerAPI.Shared
{
    public class AssignmentValidationQuery
    {
        public ApplicationDbContext _context;
        public AssignmentValidationQuery(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> IsAssignmentExist(string name)
        {
            return await _context.Assignments.AnyAsync(a => a.Name == name);
        }
        

        public async Task<bool> IsAssignmentExist(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);
            return assignment != null;
        }
        

    }
}
