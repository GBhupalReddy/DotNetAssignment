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
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            var users = await _userRepository.GetUsersAsync();
            return users;

        }

        // Get user using id
        public async Task<User> GetUserByIdAsync(int id)
        {

            var user = await _userRepository.GetUserAsync(id);
            return user;

        }

        // Add user
        public async Task<User> AddUserAsync(User user)
        {
            var result= await _userRepository.AddUserAsync(user);
            return result;
        }

        //Update user using id
        public async Task<User> UpdateUserAsynce(int id, User user)
        {
            var userToBeUpdated = await GetUserByIdAsync(id);
            userToBeUpdated.UserName = user.UserName;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.Phone = user.Phone;
            var result = await _userRepository.UpdateUserAsynce(userToBeUpdated);
            return result;

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
