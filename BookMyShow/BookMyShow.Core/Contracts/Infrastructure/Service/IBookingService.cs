using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(BookingUser user);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task<Booking> GetBookingByIdAsync(int id);
        Task<Booking> UpdateBookingAsynce(int id, Booking booking);
        Task<Booking> CreateBookingAsync(BookingUser bookingUser);
    }
}