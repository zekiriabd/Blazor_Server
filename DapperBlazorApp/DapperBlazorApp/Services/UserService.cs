using Dapper;
using DapperBlazorApp.Data.Entities;
using DapperBlazorApp.Services.Interfaces;
using System.Data;
using System.Data.SqlClient;
using System.Transactions;

namespace DapperBlazorApp.Services
{
    public class UserService : IUserService
    {
        private readonly string? con;
        public UserService(IConfiguration configuration)
        {
            con = configuration.GetConnectionString("TestDatabase");
        }

        public async Task AddUserAsync(UserDetail user)
        {
            using var db = new SqlConnection(con);
            await db.ExecuteAsync("Insert Into [User] (FirstName,LastName,Age) VALUES (@FirstName,@LastName,@Age)", user);
            //await db.ExecuteAsync("AddUser", new { user.FirstName, user.LastName, user.Age }, commandType: CommandType.StoredProcedure);
        }       

        public async Task DeleteUserAsync(int UserId)
        {
            using var db = new SqlConnection(con);
            //await db.ExecuteAsync("Delete from [User] where UserId = @id", new { UserId }, commandType: CommandType.Text);
            await db.ExecuteAsync("DeleteUserById", new { UserId }, commandType: CommandType.StoredProcedure);

        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            using var db = new SqlConnection(con);
            // return db.QueryAsync<User>("Select * From [User]");
            return await db.QueryAsync<User>("GetAllUsers", commandType: CommandType.StoredProcedure);
        }

        public async Task<UserDetail> GetUserByIdAsync(int UserId)
        {
            using var db = new SqlConnection(con);
            //return db.QueryFirstAsync<User>("Select * From [User] Where UserId = @UserId" , new { UserId });
            return await db.QueryFirstAsync<UserDetail>("GetUserById", new { UserId }, commandType: CommandType.StoredProcedure);
        }

        public async Task UpdateUserAsync(UserDetail user)
        {
            using var db = new SqlConnection(con);
            //await db.ExecuteAsync("Update [UserDetail] Set FirstName = @FirstName, LastName = @LastName, Age = @Age Where UserId = @UserId", user);
            await db.ExecuteAsync("UpdateUserById",
                new { user.UserId, user.FirstName, user.LastName, user.Age },
                commandType: CommandType.StoredProcedure);                        
        }

        public async Task AddUsersAsync(List<UserDetail> users)
        {
            using var db = new SqlConnection(con);
            if (db.State == ConnectionState.Closed) db.Open();
            var transaction = db.BeginTransaction();
            try
            {
                await db.ExecuteAsync("Insert Into [User] (FirstName,LastName,Age) VALUES (@FirstName,@LastName,@Age)", users, transaction);
                transaction.Commit();
            }
            catch(Exception ex)
            {
                transaction.Rollback();
            }
}
    }
}