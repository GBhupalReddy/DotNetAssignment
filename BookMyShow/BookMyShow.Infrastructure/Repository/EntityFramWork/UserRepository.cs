using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class UserRepository : IUserRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public UserRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }
        
        // Get all users
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var query = "select * from [User]";
            var result = await _dbConnection.QueryAsync<UserDto>(query);
            return result;
                
        }

        // Get user using id
        public async Task<User> GetUserAsync(int id)
        {
            var query = "select * from [User] where UserId = @id";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { id });
            return result;
            
        }

        // Add user
        public async Task<User> AddUserAsync(User user)
        {
            _bookMyShowContext.Users.Add(user);
           var data = await _bookMyShowContext.SaveChangesAsync();
            return user;
        }

        //Update user using id
        public async Task<User> UpdateUserAsynce(User user)
        {
          
            _bookMyShowContext.Users.Update(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;

        }

        //Delete user using id
        public async Task DeleteUserAsync(User user)
        {
            _bookMyShowContext.Users.Remove(user);
           await _bookMyShowContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id)
        {

            var UserBookingDetalisQuery = "execute GetUserBookingDetalis @id";

            var UserBookingDetalis= await _dbConnection.QueryAsync<UserBookingDto>(UserBookingDetalisQuery, new {id});
            return UserBookingDetalis;
        }
        public async Task<User> UserExitByEmail(string email)
        {
            var UserExitQuery = "select * from [User] where Email = @email";
            var UserExit = await _dbConnection.QueryFirstOrDefaultAsync<User>(UserExitQuery, new { email });
            return UserExit;
        }
        public async Task<bool> CreateUserAsync(User user)
        {
            _bookMyShowContext.Users.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
            return true;    
        }
    }
}
