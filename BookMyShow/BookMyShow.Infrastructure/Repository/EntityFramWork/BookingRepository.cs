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
            var result = await _dbConnection.QueryFirstOrDefaultAsync<Booking>(query, new { id });
            return result;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
           
                _bookMyShowContext.Bookings.Add(booking);
                await _bookMyShowContext.SaveChangesAsync();
                return booking;
          
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


      
        public async Task<int> GetcinemaHallIdAsync(int showId)
        {
            var cinemaHallIdQuery = "select cinemaHallId from Show where ShowId = @showId";

            var cinemaHallId = await _dbConnection.QueryFirstOrDefaultAsync<int>(cinemaHallIdQuery, new { showId });

            return cinemaHallId;
        }

        public async Task<IEnumerable<int>> GetCinemaSeatsAsync(int seatType,int showId)
        {
            
            var seatNumbersQery = "execute GetBookedSeats @seatType, @showId";
            var seatNumbers = await _dbConnection.QueryAsync<int>(seatNumbersQery ,new { seatType , showId});
           
            return seatNumbers;
        }

        public async Task<int> GetCinemaSeatIdAsync(int id,int cinemaHallId)
        {
            var cinemaseatid = await (from cinemaSeat in _bookMyShowContext.CinemaSeats
                                     where cinemaSeat.SeatNumber == id && cinemaSeat.CinemaHallId == cinemaHallId
                                     select cinemaSeat.CinemaSeatId).FirstAsync();
            return cinemaseatid;
        }

        public async Task<decimal> GetSeatPrice(int seatType)
        {
            var Query = "select Price from SeatTypePrice where SeatType = @seatType";
            var Price = await _dbConnection.QueryFirstOrDefaultAsync<decimal>(Query, new {seatType});

            return Price;
        }
        
    }
}
