using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using System.Data;

namespace BookMyShow.Infrastructure.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IDbConnection _dbConnection;
        public UserService(IUserRepository userRepository, IDbConnection dbConnection)
        {
            _userRepository = userRepository;
            _dbConnection = dbConnection;
        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await _userRepository.GetUsersAsync();

        }

        // Get user using id
        public async Task<User> GetUserByIdAsync(int id)
        {

            return await _userRepository.GetUserAsync(id);

        }

        // Add user
        public async Task<User> AddUserAsync(User user)
        {
            return await _userRepository.AddUserAsync(user);
        }

        //Update user using id
        public async Task<User> UpdateUserAsynce(int id, User user)
        {
            var userToBeUpdated = await GetUserByIdAsync(id);
            userToBeUpdated.UserName = user.UserName;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.Phone = user.Phone;
            return await _userRepository.UpdateUserAsynce(userToBeUpdated);

        }

        //Delete user using id
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            await _userRepository.DeleteUserAsync(user);
        }
        public async Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id)
        {
            var result=await _userRepository.GetUserBookingDetalisAsync(id);
            return result;
        }
    }
}
