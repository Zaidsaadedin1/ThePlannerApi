using ThePlannerAPI.DTOs.User;
using ThePlannerAPI.Enums;
using ThePlannerAPI.Model;

namespace ThePlannerAPI.DTOs.Assignment
{
    public class GetAssignmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public AssignmentProiority Priority { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } 
        public int CategoryId { get; set; }

    }
}
