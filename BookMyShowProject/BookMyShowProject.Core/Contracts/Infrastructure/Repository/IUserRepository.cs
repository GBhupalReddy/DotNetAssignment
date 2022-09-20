using BookMyShowProject.Core.Entities;

namespace BookMyShowProject.Core.Contracts.infrastructure.Repository
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<IEnumerable<User>> GetUsersAsync(int Id);
    }
}