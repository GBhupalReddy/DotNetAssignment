using AutoMapper;
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
        private readonly IMapper _mapper;
        private readonly IShowSeatRepository _showSeatRepository;
        public BookingRepository(BookMyShowContext bookMyShowContext,IDbConnection dbConnection, IMapper mapper, IShowSeatRepository showSeatRepository)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
            _mapper = mapper;
            _showSeatRepository = showSeatRepository;
        }


        // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var query = "execute GetBooking";
            var result = await _dbConnection.QueryAsync<BookingDto>(query);
            return result;
        }
        
        // Get  Booking using booking id
        public async Task<Booking> GetBookingAsync(int id)
        {

            var query = "execute GetBookingById  @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Booking>(query, new { id }));
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


      
        public async Task<int> GetcinemaHallId(BookingUser bookingUser)
        {

            var cinemaHallId = await (from show in _bookMyShowContext.Shows
                                      where show.ShowId == bookingUser.ShowId
                                      select show.CinemaHallId).FirstOrDefaultAsync();
            return cinemaHallId;
        }

        public async Task<List<int?>> GetCinemaSeats(int seatType,BookingUser bookingUser)
        {
            var seatNumbers = await (from show in _bookMyShowContext.Shows
                                     join showSeat in _bookMyShowContext.ShowSeats
                                     on show.ShowId equals showSeat.ShowId
                                     join cinemaSeat in _bookMyShowContext.CinemaSeats
                                     on showSeat.CinemaSeatId equals cinemaSeat.CinemaSeatId
                                     where showSeat.ShowId == bookingUser.ShowId && cinemaSeat.Type == seatType
                                     select cinemaSeat.SeatNumber).ToListAsync();
            return seatNumbers;
        }
        public async Task<IEnumerable<ShowSeat>> GetBookedTickets(decimal a, BookingUser bookingUser)
        {
            var bookedTickets = await (from shoeSeat in _bookMyShowContext.ShowSeats
                                where shoeSeat.Price == a && shoeSeat.ShowId == bookingUser.ShowId
                                select shoeSeat).ToListAsync();
            return bookedTickets;
        }
        public async Task<int> GetCinemaSeatId(int id,int cinemaHallId)
        {
            var cinemaseatid = await (from cinemaSeat in _bookMyShowContext.CinemaSeats
                                     where cinemaSeat.SeatNumber == id && cinemaSeat.CinemaHallId == cinemaHallId
                                     select cinemaSeat.CinemaSeatId).FirstAsync();
            return cinemaseatid;
        }
        
    }
}
