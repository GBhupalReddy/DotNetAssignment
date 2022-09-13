using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public BookingRepository(BookMyShowContext bookMyShowContext,IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }


       // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var query = "select * from Booking";
            var result = await _dbConnection.QueryAsync<BookingDto>(query);
            return result;
        }
        
        // Get  Booking using booking id
        public async Task<Booking> GetBookingAsync(int id)
        {

            var query = "select * from Booking where BookingId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Booking>(query, new { id }));
            return result;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            var availableSeats = await (from show in _bookMyShowContext.Shows
                                join cinemaHall in _bookMyShowContext.CinemaHalls
                                on show.CinemaHallId equals cinemaHall.CinemaHallId
                                where show.ShowId == booking.ShowId
                                select cinemaHall.AvailableSeats).FirstOrDefaultAsync();
            if (availableSeats >= booking.NumberOfSeats)
            {
                _bookMyShowContext.Bookings.Add(booking);
                await _bookMyShowContext.SaveChangesAsync();
                return booking;
            }
            return null;
        }

        // Update booking using id
        public async Task<Booking> UpdateBookingAsynce(Booking booking)
        {
            
            _bookMyShowContext.Bookings.Update(booking);
            await _bookMyShowContext.SaveChangesAsync();
            return booking;

        }

        //deleted booking using id
        public async Task DeleteBookingAsync(Booking booking)
        {
            _bookMyShowContext.Bookings.Remove(booking);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
