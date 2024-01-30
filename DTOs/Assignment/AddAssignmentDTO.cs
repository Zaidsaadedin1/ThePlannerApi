    using ThePlannerAPI.Enums;

namespace ThePlannerAPI.DTOs.Task
{
    public class AddAssignmentDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public AssignmentProiority Priority { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CategoryId { get; set; }


    }
}
