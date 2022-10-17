using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IUserService
    {
        Task<string> AddUserAsync(User user);
        Task DeleteUserAsync(int id);
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<User> UpdateUserAsynce(int id, User user);
        Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id);
        Task<User> UserExitByEmail(string email);
        Task<bool> CreateUserAsync(User user);
        Task<bool> Login(string email, string password);
    }
}