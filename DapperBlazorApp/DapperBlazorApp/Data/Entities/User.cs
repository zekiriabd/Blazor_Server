namespace DapperBlazorApp.Data.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }

    public class UserDetail : User
    {
        public int Age { get; set; }
    }
}