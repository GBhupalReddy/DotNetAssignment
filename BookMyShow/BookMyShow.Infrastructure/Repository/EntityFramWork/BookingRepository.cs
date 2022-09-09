using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
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
            //var result = await _dbConnection.QueryFirstAsync<Booking>(query, new {id=id});
            return result;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking user)
        {
            _bookMyShowContext.Bookings.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;
        }

        // Update booking using id
        public async Task<Booking> UpdateBookingAsynce(int id, Booking booking)
        {
            var bookingToBeUpdated = await GetBookingAsync(id);
            bookingToBeUpdated.NumberOfSeats = booking.NumberOfSeats;
            bookingToBeUpdated.Timestamp = booking.Timestamp;
            bookingToBeUpdated.Status = booking.Status;
            bookingToBeUpdated.UserId = booking.UserId;
            bookingToBeUpdated.ShowId = booking.ShowId;
            _bookMyShowContext.Bookings.Update(bookingToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return bookingToBeUpdated;

        }

        //deleted booking using id
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await GetBookingAsync(id);
            _bookMyShowContext.Bookings.Remove(booking);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
