        namespace ThePlannerAPI.Model
    {
        public class Category
        {
            public int Id { get; set; }
            public string CategoryName { get; set; } = null!;

            public List<Assignment>? Assignments { get; set; }
        }
    }
