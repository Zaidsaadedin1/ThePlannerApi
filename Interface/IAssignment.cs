using ThePlannerAPI.DTOs.Assignment;
using ThePlannerAPI.DTOs.Task;
using ThePlannerAPI.Enums;

namespace ThePlannerAPI.Interface
{
    public interface IAssignment
    {   

        Task<List<GetAssignmentDTO>> GetAllAssignments();
        Task<GetAssignmentDTO?> GetAssignmentById(int id);
        Task<int> UpdateAssignment(UpdateAssignmentDTO updateAssignment, int id);
        Task<int> DeleteAssignment(int assignmentID);
        Task<int> AddAssignment(AddAssignmentDTO assignment);
        Task<List<GetAssignmentDTO>> GetAssignmentsByFilter(AssignmentFilterDTO filter);
        Task<List<GetAssignmentDTO>> GetAssignmentsBySearch(string searchValue);


    }
}
    