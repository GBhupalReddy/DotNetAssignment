using BookMyShowProject.Core.Contracts.infrastructure.Repository;
using BookMyShowProject.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookMyShowProject.infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BookMYShowContext _bookMyShowContext;
        public UserRepository(BookMYShowContext bookMyShowContext)
        {
            _bookMyShowContext = bookMyShowContext;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int Id)
        {
            return await (from useres in _bookMyShowContext.Users
                          select useres).ToListAsync();
        }
        public async Task AddUserAsync(User user)
        {
            _bookMyShowContext.Users.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
           
        }
    }
}
