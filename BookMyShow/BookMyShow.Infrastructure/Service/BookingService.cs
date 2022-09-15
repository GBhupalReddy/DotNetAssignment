using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IShowSeatRepository _showSeatRepository;

        public BookingService( IBookingRepository bookingRepository, IMapper mapper, IShowSeatRepository showSeatRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _showSeatRepository = showSeatRepository;
        }

        // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return bookings;
        }

        // Get  Booking using booking id
        public async Task<Booking> GetBookingUsingIdAsync(int id)
        {

            var booking = await _bookingRepository.GetBookingAsync(id);
            return booking;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            var reusult = await _bookingRepository.AddBookingAsync(booking);
            return reusult;
        }

        // Update booking using id
        public async Task<Booking> UpdateBookingAsynce(int id, Booking booking)
        {
            var bookingToBeUpdated = await GetBookingUsingIdAsync(id);
            bookingToBeUpdated.NumberOfSeats = booking.NumberOfSeats;
            bookingToBeUpdated.Timestamp = booking.Timestamp;
            bookingToBeUpdated.Status = booking.Status;
            bookingToBeUpdated.UserId = booking.UserId;
            bookingToBeUpdated.ShowId = booking.ShowId;

            var reusult = await _bookingRepository.UpdateBookingAsynce(bookingToBeUpdated);
            return reusult;

        }

        //deleted booking using id
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await GetBookingUsingIdAsync(id);
            await _bookingRepository.DeleteBookingAsync(booking);
        }

        public async Task<Booking> CreateBooking(BookingUser bookingUser)
        {
            
                int seatType = bookingUser.SeatType;
                var seatNumbers = await _bookingRepository.GetCinemaSeats(seatType, bookingUser);
                decimal a = 0;
                int cinemaHallId = await _bookingRepository.GetcinemaHallId(bookingUser);

                List<int> thirdClass = new List<int> { 1, 2 };
                List<int> secondClass = new List<int> { 3, 4 };
                List<int> firstClass = new List<int> { 5, 6 };

                if (seatType == 1)
                {
                    a = 200;
                    foreach (var ab in seatNumbers)
                    {
                            thirdClass.Remove((int)ab);
                    }
                }
                 if (seatType == 2)
                 {
                   a = 300;
                     foreach (var ab in seatNumbers)
                     {
                     secondClass.Remove((int)ab);
                     }
                 }
                 if (seatType == 3)
                 {
                      a = 400;
                     foreach (var ab in seatNumbers)
                     {
                         firstClass.Remove((int)ab);
                     }
                 }
               
                 var vdata = _mapper.Map<BookingUser, Booking>(bookingUser);
                 var data = await AddBookingAsync(vdata);

                 if (data != null)
                 {
                     
                     if(seatType == 1)
                     {
                         for (int i = 0; i < bookingUser.NumberOfSeats; i++)
                         {
                             var cinemaseaid = await _bookingRepository.GetCinemaSeatId(thirdClass[i], cinemaHallId);
                             ShowSeat showSeat1 = new()
                             {
                                 ShowSeatId = 0,
                                 Status = 1,
                                 Price = a,
                                 CinemaSeatId = cinemaseaid,
                                 ShowId = bookingUser.ShowId,
                                 BookingId = data.BookingId,
                             };
                             await _showSeatRepository.AddShowSeatAsync(showSeat1);
                         }
                     }
                     if (seatType == 1)
                     {
                       for (int i = 0; i < bookingUser.NumberOfSeats; i++)
                        {
                             var cinemaseaid = await _bookingRepository.GetCinemaSeatId(secondClass[i], cinemaHallId);
                             ShowSeat showSeat1 = new()
                             {
                                 ShowSeatId = 0,
                                 Status = 1,
                                 Price = a,
                                 CinemaSeatId = cinemaseaid,
                                 ShowId = bookingUser.ShowId,
                                 BookingId = data.BookingId,
                             };

                             await _showSeatRepository.AddShowSeatAsync(showSeat1);

                         }
                      }
                     if (seatType == 1)
                     {
                         for (int i = 0; i < bookingUser.NumberOfSeats; i++)
                         {
                             var cinemaseaid = await _bookingRepository.GetCinemaSeatId(firstClass[i], cinemaHallId);
                             ShowSeat showSeat1 = new()
                             {
                                 ShowSeatId = 0,
                                 Status = 1,
                                 Price = a,
                                 CinemaSeatId = cinemaseaid,
                                 ShowId = bookingUser.ShowId,
                                 BookingId = data.BookingId,
                             };

                             await _showSeatRepository.AddShowSeatAsync(showSeat1);

                         }

                     }

                 return data;
                }
            
            return null;
        }
       
    }
}

