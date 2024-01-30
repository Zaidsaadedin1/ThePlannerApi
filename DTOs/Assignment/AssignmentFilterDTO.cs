using ThePlannerAPI.Enums;

namespace ThePlannerAPI.DTOs.Assignment
{
    public class AssignmentFilterDTO
    {
        public AssignmentProiority? Priority { get; set; }
        public string? Name { get; set; } 
        public bool? IsComplete { get; set; }
    }
}
