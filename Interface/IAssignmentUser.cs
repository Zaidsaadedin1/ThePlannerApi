using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.User;
using ThePlannerAPI.DTOs.UserAssignment;

namespace ThePlannerAPI.Interface
{
    public interface IAssignmentUser
    {
        Task<List<GetUserDTO>?> GetAllUsersAssignToAssignment(int assignmentID);
        Task<List<GetAssignmentDTO>?> GetAllAssignmentAssignToUser(string userId);
        Task<int> AssignAssignmentToUsers(List<GetUserDTO> users, int assignmentId);
        Task<int> UnAssignUserFromAssignment(string userId, int assignmentId);
    }
}
