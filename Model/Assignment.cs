using ThePlannerAPI.Enums;
using FluentValidation;
namespace ThePlannerAPI.Model
{
    public class Assignment
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public AssignmentProiority Priority { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? StartDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool IsCompleted { get; set; } = false;
        public int CategoryId { get; set; } 

        // Navigation Properties
        public List<User> AssignedUsers { get; set; } = null!;
        public Category? Category { get; set; }
    }


}
