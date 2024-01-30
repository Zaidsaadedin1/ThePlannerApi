using Microsoft.EntityFrameworkCore;
using ThePlannerAPI.Context;
using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.User;
using ThePlannerAPI.DTOs.UserAssignment;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Interface;
using ThePlannerAPI.Model;

namespace ThePlannerAPI.Services
{
    public class AssignmentUsersService : IAssignmentUser
    {
        public readonly ApplicationDbContext _context;
        public AssignmentUsersService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> AssignAssignmentToUsers(List<GetUserDTO> users, int assignmentId)
        {


            var assignment = await _context.Assignments.FindAsync(assignmentId);
            foreach (var assignedUser in users)
            {
                var user = await _context.Users
                .Include(u => u.Assignments)
                .FirstAsync(u => u.Id == assignedUser.Id);
                user.Assignments.Add(assignment!);
                await _context.SaveChangesAsync();
            }

            return (int)ApiResponseStatus.AssignAssignmentToUser;
        }

        public async Task<List<GetAssignmentDTO>?> GetAllAssignmentAssignToUser(string userId)
        {
            var user = await _context.Users
                            .Include(u => u.Assignments)
                            .FirstOrDefaultAsync(u => u.Id == userId);


            var assignmentDTOs = user?.Assignments
                .Select(a => new GetAssignmentDTO
                {
                    Id = a.Id,
                    Name = a.Name,
                    Description = a.Description,
                    Priority = a.Priority,
                    CreatedAt = a.CreatedAt,
                    StartDate = a.StartDate,
                    DueDate = a.DueDate,
                    IsCompleted = a.IsCompleted,
                    CategoryId = a.CategoryId
                })
                .ToList();

            return assignmentDTOs;
        }


        public async Task<List<GetUserDTO>?> GetAllUsersAssignToAssignment(int assignmentId)
        {
            var assignment = await _context.Assignments
           .Include(a => a.AssignedUsers)
           .FirstOrDefaultAsync(a => a.Id == assignmentId);


            var userDTOs = assignment?.AssignedUsers
                .Select(u => new GetUserDTO
                {
                    Id = u.Id,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Username = u.Username,
                    HasImage = u.HasImage,
                    ImageUrl = u.ImageUrl,
          
                })
                .ToList();

            return userDTOs;

        }

        public async Task<int> UnAssignUserFromAssignment(string userId, int assignmentId)
        {
            var user = await _context.Users.FindAsync(userId);
           
            var assignment = await _context.Assignments
            .Include(a => a.AssignedUsers)
            .FirstOrDefaultAsync(a => a.Id == assignmentId);
            
            user!.Assignments.Remove(assignment!);
            await _context.SaveChangesAsync();          

            return (int)ApiResponseStatus.unAssignUserToAssignment;

        }
    }
}


