using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking user);
        Task DeleteBookingAsync(int id);
        Task<Booking> GetBookingAsync(int id);
        Task<IEnumerable<Booking>> GetBookingsAsync();
        Task<Booking> UpdateBookingAsynce(int id, Booking booking);
    }
}