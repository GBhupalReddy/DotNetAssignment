using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using System.Text.Json;

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
        public async Task<string> AddUserAsync(User user)
        {
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("https://localhost:7281/");
            //    var content = new StringContent(JsonSerializer.Serialize(user), System.Text.Encoding.UTF8, "application/json");
            //    var result = await client.PostAsync("/auth/register", content);
            //    //if(!result.IsSuccessStatusCode)

            //    string resultContent = await result.Content.ReadAsStringAsync();
            //    return resultContent;
            //}

            var userResult = await _userRepository.AddUserAsync(user);
            if (userResult != null)
                return "true";
            return "false";
        }

        //Update user using id
        public async Task<User> UpdateUserAsynce(int id, User user)
        {
            var userToBeUpdated = await GetUserByIdAsync(id);
            userToBeUpdated.UserName = user.UserName;
            userToBeUpdated.Email = user.Email;
            userToBeUpdated.Password = user.Password;
            userToBeUpdated.Phone = user.Phone;
            var userResult = await _userRepository.UpdateUserAsynce(userToBeUpdated);
            return userResult;

        }

        //Delete user using id
        public async Task DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);
            await _userRepository.DeleteUserAsync(user);
        }
        public async Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id)
        {
            var result = await _userRepository.GetUserBookingDetalisAsync(id);
            return result;
        }

        public async Task<User> UserExitByEmail(string email)
        {
            var userExit = await _userRepository.UserExitByEmail(email);

            return userExit;

        }

        public async Task<bool> CreateUserAsync(User user)
        {
            var userResult = await _userRepository.CreateUserAsync(user);
            return userResult;
        }
        public async Task<bool> Login(string email, string password)
        {
            var user = await _userRepository.UserExitByEmail(email);

            return true;
        }
    }
}
