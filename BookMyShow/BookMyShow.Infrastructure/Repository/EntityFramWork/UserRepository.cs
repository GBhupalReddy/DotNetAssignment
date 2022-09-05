using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class UserRepository : IUserRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public UserRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }
        

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await (from user in _bookMyShowContext.Users

                          select new UserDto
                          {
                              UserId = user.UserId,
                              Name = user.Name,
                              Email = user.Email,
                              Passoword = user.Passoword,
                              Phone = user.Phone
                          }).ToListAsync();

        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _bookMyShowContext.Users.FindAsync(id);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _bookMyShowContext.Users.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;
        }
        public async Task<User> UpdateUserAsynce(int id,User user)
        {
            var userToBeUpdated=await GetUserAsync(id);
            userToBeUpdated.Name= user.Name;
            userToBeUpdated.Email= user.Email;
            userToBeUpdated.Passoword= user.Passoword;  
            userToBeUpdated.Phone= user.Phone;
            _bookMyShowContext.Users.Update(userToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return userToBeUpdated;

        }

        public async Task DeleteUserAsync(int id)
        {
            var user= await GetUserAsync(id);
            _bookMyShowContext.Users.Remove(user);
           await _bookMyShowContext.SaveChangesAsync();
        }
        
    }
}
