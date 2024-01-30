namespace ThePlannerAPI.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;

        // Navigation Properties
        public List<Assignment>? Assignments { get; set; }
       // int UserId { get; set; }
       //User user {get; set;}


    }
}
