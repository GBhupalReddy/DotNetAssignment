using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking user);
        Task DeleteBookingAsync(int id);
        Task<Booking> GetBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task<Booking> UpdateBookingAsynce(int id, Booking booking);
    }
}