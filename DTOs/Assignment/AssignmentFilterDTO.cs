using ThePlannerAPI.Enums;

namespace ThePlannerAPI.DTOs.Assignment
{
    public class AssignmentFilterDTO
    {
        public AssignmentProiority? Priority { get; set; }
        public int? CategoryId { get; set; } 
        public bool? IsComplete { get; set; }
    }
}
