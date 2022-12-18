using DapperBlazorApp.Data.Entities;

namespace DapperBlazorApp.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<UserDetail> GetUserByIdAsync(int id);

        Task DeleteUserAsync(int id);
        
        Task AddUserAsync(UserDetail user);       
        Task AddUsersAsync(List<UserDetail> users);

        Task UpdateUserAsync(UserDetail user);
        

    }
}
