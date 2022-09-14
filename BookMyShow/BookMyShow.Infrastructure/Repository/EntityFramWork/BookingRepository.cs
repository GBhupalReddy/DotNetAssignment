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
                                        where show.ShowId == booking.ShowId
                                        select show.AvailableSeats).FirstOrDefaultAsync();
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

        public async Task<Booking> CreateBooking(BookingUser bookingUser)
        {
            var availableSeats = await (from show in _bookMyShowContext.Shows
                                        where show.ShowId == bookingUser.ShowId
                                        select show.AvailableSeats).FirstOrDefaultAsync();
            var cinemaHallId = await(from show in _bookMyShowContext.Shows
                                     where show.ShowId == bookingUser.ShowId
                                     select show.CinemaHallId).FirstOrDefaultAsync(); 

            int numberOfTickets = bookingUser.NumberOfSeats;
            if (availableSeats >= numberOfTickets)
            {
                int seatType = bookingUser.SeatType;
                var seatNumbers = await (from show in _bookMyShowContext.Shows
                                         join cinemahall in _bookMyShowContext.CinemaHalls
                                         on show.CinemaHallId equals cinemahall.CinemaHallId
                                         join cinemaSeat in _bookMyShowContext.CinemaSeats
                                         on cinemahall.CinemaHallId equals cinemaSeat.CinemaHallId
                                         where show.ShowId == bookingUser.ShowId
                                         select cinemaSeat.SeatNumber).ToListAsync();



                int classAvailableSeats = 0;
                int[] thirdClass = { 1, 2 };
                int[] secondClass = { 3, 4 };
                int[] firstClass = { 5, 6 };
                List<int> ids = new List<int>();
                foreach (var ab in seatNumbers)
                {
                    
                    if (seatType == 1)
                    {
                        bool thirdClassReault = Array.Exists(thirdClass, element => element == ab);
                        if (thirdClassReault)
                        {
                            classAvailableSeats++;
                            ids.Add((int)ab);
                        }
                    }
                    if (seatType == 2)
                    {
                        bool secondClassResult = Array.Exists(secondClass, element => element == ab);
                        if (secondClassResult)
                        {
                            classAvailableSeats++;
                            ids.Add((int)ab);
                        }
                    }
                    if (seatType == 3)
                    {
                        bool firstClassresult = Array.Exists(firstClass, element => element == ab);
                        if (firstClassresult)
                        {
                            classAvailableSeats++;
                            ids.Add((int)ab);
                        }
                    }
                }

                if (classAvailableSeats >= numberOfTickets)
                {
                    var vdata = _mapper.Map<BookingUser, Booking>(bookingUser);
                    var data = await AddBookingAsync(vdata);

                    if (data != null)
                    {
                        foreach (var id in ids)
                        {

                            var cinemaseaid = await (from cinemaSeat in _bookMyShowContext.CinemaSeats
                                                     where cinemaSeat.SeatNumber == id && cinemaSeat.CinemaHallId == cinemaHallId
                                                     select cinemaSeat.CinemaSeatId).FirstAsync();

                            ShowSeat showSeat1 = new()
                            {
                                Status = 1,
                                CinemaSeatId = cinemaseaid,
                                ShowId = bookingUser.ShowId,
                                BookingId = data.BookingId,
                            };

                            await _showSeatRepository.AddShowSeatAsync(showSeat1);
                        }
                    }

                    return data;

                }
            }
            return null;
        }

        public async Task<IEnumerable<CinemaSeat>> getdata()
        {
            var seatNumbers = await (from show in _bookMyShowContext.Shows
                                     join cinemahall in _bookMyShowContext.CinemaHalls
                                     on show.CinemaHallId equals cinemahall.CinemaHallId
                                     join cinemaSeat in _bookMyShowContext.CinemaSeats
                                     on cinemahall.CinemaHallId equals cinemaSeat.CinemaHallId
                                     where show.ShowId == 6 
                                     select cinemaSeat).ToListAsync();

            return seatNumbers;
        }
    }
}
