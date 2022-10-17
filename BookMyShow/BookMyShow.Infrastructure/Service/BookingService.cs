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

        public BookingService(IBookingRepository bookingRepository, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return bookings;
        }

        // Get  Booking using booking id
        public async Task<Booking> GetBookingByIdAsync(int id)
        {

            var booking = await _bookingRepository.GetBookingAsync(id);
            return booking;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(BookingUser bookingUser)
        {
            var booking = _mapper.Map<BookingUser, Booking>(bookingUser);
            booking.Timestamp = DateTime.Now;
            booking.Status = 0;
            var reusult = await _bookingRepository.AddBookingAsync(booking);
            return reusult;
        }

        // Update booking using id
        public async Task<Booking> UpdateBookingAsynce(int id, Booking booking)
        {
            var bookingToBeUpdated = await GetBookingByIdAsync(id);
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
            var booking = await GetBookingByIdAsync(id);
            await _bookingRepository.DeleteBookingAsync(booking);


        }

        public async Task<Booking?> CreateBookingAsync(BookingUser bookingUser)
        {
            var availableSeats = await _bookingRepository.GetAvailableSeats(bookingUser.ShowId);
            int availableSeatscount = 0;

            //check seats are available are not

            if (bookingUser.SeatType == 1)
            {
                availableSeatscount = availableSeats.Firstclass;
            }
            else if (bookingUser.SeatType == 2)
            {
                availableSeatscount = availableSeats.SecondClass;
            }
            else if (bookingUser.SeatType == 3)
            {
                availableSeatscount = availableSeats.ThirdClass;
            }

            if (availableSeatscount >= bookingUser.NumberOfSeats)
            {
                var result = await AddBookingAsync(bookingUser);

                return result;
            }


            return null;
        }


    }
}

