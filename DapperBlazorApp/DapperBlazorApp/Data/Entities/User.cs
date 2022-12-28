using Radzen.Blazor;

namespace DapperBlazorApp.Data.Entities
{
   
    public class Category
    {
        public int CategoryId { get; set; }
        public string? Name { get; set; }
        public List<User> Users { get; set; }
        public Category() { Users = new List<User>(); }
    }
    public class User
    {
        public int UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Role> Roles { get; set; }
        public User() { Roles = new List<Role>(); }
    }
    public class UserDetail : User
    {
        public int Age { get; set; }
        public int CategoryId { get; set; }
    }

    public class Role
    {
        public int id { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
    }

    
}