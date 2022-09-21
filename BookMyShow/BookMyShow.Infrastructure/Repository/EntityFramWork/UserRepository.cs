using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
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
            var query = "execute GetUsers";
            var result = await _dbConnection.QueryAsync<UserDto>(query);
            return result;
                
        }

        // Get user using id
        public async Task<User> GetUserAsync(int id)
        {
            var query = "execute GetUserById";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<User>(query, new { id }));
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
        public async Task<User> UpdateUserAsynce(User user)
        {
          
            _bookMyShowContext.Users.Update(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;

        }

        //Delete user using id
        public async Task DeleteUserAsync(User user)
        {
            _bookMyShowContext.Users.Remove(user);
           await _bookMyShowContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<UserBookingDto>> GetUserBookingDetalisAsync(int id)
        {
            var result = await (from user in _bookMyShowContext.Users
                                join booking in _bookMyShowContext.Bookings
                                on user.UserId equals booking.UserId
                                join payment in _bookMyShowContext.Payments
                                on booking.BookingId equals payment.BookingId
                                join show in _bookMyShowContext.Shows
                                on booking.ShowId equals show.ShowId
                                join cinemahall in _bookMyShowContext.CinemaHalls
                                on show.CinemaHallId equals cinemahall.CinemaHallId
                                join cinema in _bookMyShowContext.Cinemas
                                on cinemahall.CinemaId equals cinema.CinemaId
                                join city in _bookMyShowContext.Cities
                                on cinema.CityId equals city.CityId
                                where user.UserId == id
                                select new UserBookingDto
                                {
                                    UserId = user.UserId,
                                    Name = user.UserName,
                                    Passoword = user.Password,
                                    Email = user.Email,
                                    Phone = user.Phone,
                                    BookingId = booking.BookingId,
                                    NumberOfSeats = booking.NumberOfSeats,
                                    Timestamp = booking.Timestamp,
                                    PaymentId = payment.PaymentId,
                                    Amount = payment.Amount,
                                    DicountCoupon = payment.DicountCoupon,
                                    RemoteTransactionId = payment.RemoteTransactionId,
                                    CinemaHallName = cinemahall.CinemaHallName,
                                    CinemaName = cinema.CinemaName,
                                    CityName = city.CityName,
                                }).ToListAsync();
            return result;
        }
        
    }
}
