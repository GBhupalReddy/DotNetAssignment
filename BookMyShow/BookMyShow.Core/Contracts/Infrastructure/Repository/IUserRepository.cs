using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> AddUserAsync(User user);
        Task<User> UpdateUserAsynce(int id, User user);
        Task DeleteUserAsync(int id);
    }
}
