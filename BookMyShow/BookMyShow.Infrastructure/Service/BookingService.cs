using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        
        public BookingService( IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
            
        }

        // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            return await _bookingRepository.GetBookingsAsync();
        }

        // Get  Booking using booking id
        public async Task<Booking> GetBookingUsingIdAsync(int id)
        {

            return await _bookingRepository.GetBookingAsync(id);
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking user)
        {
            return await _bookingRepository.AddBookingAsync(user);
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
    }
}

