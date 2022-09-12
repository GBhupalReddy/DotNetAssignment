using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IBookingService
    {
        Task<Booking> AddBookingAsync(Booking user);
        Task DeleteBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task<Booking> GetBookingUsingIdAsync(int id);
        Task<Booking> UpdateBookingAsynce(int id, Booking booking);
    }
}