using Microsoft.EntityFrameworkCore;
using ThePlannerAPI.Context;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.Task;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;
using ThePlannerAPI.Shared;

namespace ThePlannerAPI.Services
{
    public class AssignmentService : IAssignment
    {
        public ApplicationDbContext _context;
        public AssignmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AddAssignment(AddAssignmentDTO assignment)
        {
            var newAssignment = new Assignment
            {
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority,
                StartDate = assignment.StartDate,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
                CategoryId = assignment.CategoryId,
                CreatedAt = DateTime.Now
            };

            _context.Assignments.Add(newAssignment);
            await _context.SaveChangesAsync();

            return (int)ApiResponseStatus.Created;
        }



        public async Task<int> DeleteAssignment(int assignmentID)
        {
            var deletedAssignment = await _context.Assignments.FindAsync(assignmentID);

            _context.Assignments.RemoveRange(deletedAssignment!);
            await _context.SaveChangesAsync();

            return (int)ApiResponseStatus.Deleted;
        }


        public async Task<List<GetAssignmentDTO>> GetAllAssignments()
        {
            var assignments = await _context.Assignments.ToListAsync();
            var AllAssignments = assignments.Select(assignment => new GetAssignmentDTO
            {
                Id = assignment.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority,
                StartDate = assignment.StartDate,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
                CategoryId = assignment.CategoryId
            }).ToList();

            return AllAssignments;
        }

        public async Task<GetAssignmentDTO?> GetAssignmentById(int id)
        {
            var assignment = await _context.Assignments.FindAsync(id);

            var assignmentDTO = new GetAssignmentDTO
            {
                Id = assignment!.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority,
                StartDate = assignment.StartDate,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
                CategoryId = assignment.CategoryId
              
            };

            return assignmentDTO;
        }


        public async Task<int> UpdateAssignment(UpdateAssignmentDTO updateAssignment, int categoryId)
        {
            var existingAssignment = await _context.Assignments.FirstOrDefaultAsync(a => a.Id == categoryId);

            existingAssignment!.Name = updateAssignment.Name;
            existingAssignment.Description = updateAssignment.Description;
            existingAssignment.IsCompleted = updateAssignment.IsCompleted;
            existingAssignment.StartDate = updateAssignment.StartDate;
            existingAssignment.DueDate = updateAssignment.DueDate;
            existingAssignment.Priority = updateAssignment.Priority;
            existingAssignment.IsCompleted = updateAssignment.IsCompleted;
            existingAssignment.CategoryId = updateAssignment.CategoryId;
                
            await _context.SaveChangesAsync();

            return (int)ApiResponseStatus.Updated; ;
        }
        public async Task<List<GetAssignmentDTO>> GetAssignmentsByFilter(AssignmentFilterDTO filter)
        {
            var query = _context.Assignments.AsQueryable();

            if (filter.CategoryId != null)
            {
                query = query.Where(a => a.CategoryId == filter.CategoryId);
            }

            if (filter.IsComplete.HasValue)
            {
                query = query.Where(a => a.IsCompleted == filter.IsComplete);
            }

            if (filter.Priority.HasValue)
            {
                query = query.Where(a => a.Priority == filter.Priority);
            }

            var filteredAssignments = await query
                .Select(a => new GetAssignmentDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Priority = a.Priority,
                    StartDate = a.StartDate,
                    DueDate = a.DueDate,
                    IsCompleted = a.IsCompleted,
                    CategoryId= a.CategoryId
                })
                .ToListAsync();

            return filteredAssignments;
        }

        public async Task<List<GetAssignmentDTO>> GetAssignmentsBySearch(string searchValue)
        {
            var assignments = await _context.Assignments
                .Where(a => a.Description.Contains(searchValue) || a.Name.Contains(searchValue))
                .ToListAsync();

            var allAssignments = assignments.Select(assignment => new GetAssignmentDTO
            {
                Id = assignment.Id,
                Name = assignment.Name,
                Description = assignment.Description,
                Priority = assignment.Priority,
                StartDate = assignment.StartDate,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
                CategoryId=assignment.CategoryId
                
            }).ToList();

            return allAssignments;
        }

    }
}
