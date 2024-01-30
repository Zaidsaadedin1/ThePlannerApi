namespace ThePlannerAPI.Model
{
    public class User
    {
        public string Id { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool HasImage { get; set; } = false;
        public string? ImageUrl { get; set; }    



        // Navigation Properties
        public List<Assignment> Assignments { get; set; } = null!;
        // public List<Category> Categories {get; set;} = null!;


    }
}
