using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Service
{
    public class UserBookingDetailsService : IUserBookingDetailsService
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public UserBookingDetailsService(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
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
                                    Name = user.Name,
                                    Passoword = user.Passoword,
                                    Email = user.Email,
                                    Phone = user.Phone,
                                    BookingId = booking.BookingId,
                                    NumberOfSeats = booking.NumberOfSeats,
                                    Timestamp = booking.Timestamp,
                                    PaymentId = payment.PaymentId,
                                    Amount=payment.Amount,
                                    DicountCoupon=payment.DicountCoupon,
                                    RemoteTransactionId=payment.RemoteTransactionId,
                                    CinemaHallName=cinemahall.Name,
                                    CinemaName=cinema.Name,
                                    CityName=city.Name,
                                }).ToListAsync();
            return result;
        }
    }
}
