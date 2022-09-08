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
            var query = "select * from [User] where UserId=@id";
            var result = (await _dbConnection.QueryAsync<User>(query, new { id })).FirstOrDefault();
          //  var result = await _dbConnection.QueryFirstAsync<User>(query,new { id = id });
            return result;
            
        
        }

        // Add user
        public async Task<User> AddUserAsync(User user)
        {
            _bookMyShowContext.Users.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;
        }

        //Update user using id
        public async Task<User> UpdateUserAsynce(int id,User user)
        {
            var userToBeUpdated=await GetUserAsync(id);
            userToBeUpdated.UserName= user.UserName;
            userToBeUpdated.Email= user.Email;
            userToBeUpdated.Password = user.Password;  
            userToBeUpdated.Phone= user.Phone;
            _bookMyShowContext.Users.Update(userToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return userToBeUpdated;

        }

        //Delete user using id
        public async Task DeleteUserAsync(int id)
        {
            var user= await GetUserAsync(id);
            _bookMyShowContext.Users.Remove(user);
           await _bookMyShowContext.SaveChangesAsync();
        }
        
    }
}
