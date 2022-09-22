using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsynce( User user);
        Task DeleteUserAsync(User user);
        Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id);
        Task<User> UserExitByEmail(string email);
    }
}
