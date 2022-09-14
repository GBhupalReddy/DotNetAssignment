using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Core.Enums;

namespace BookMyShow.Infrastructure.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;

        public BookingService( IBookingRepository bookingRepository,IMapper mapper)
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

        public async Task<bool> CreateBooking(BookingUser bookingUser)
        {
            int numberOfTickets = bookingUser.NumberOfSeats;
            int seatType = bookingUser.SeatType;
            return  false;
        }
    }
}

