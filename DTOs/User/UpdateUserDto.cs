namespace ThePlannerAPI.DTOs.User
{
    public class UpdateUserDto
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Username { get; set; } = null!;
        public bool HasImage { get; set; }
        public string? ImageUrl { get; set; }
   
    }
}
