using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IBookingRepository
    {
        Task<Booking> AddBookingAsync(Booking user);
        Task DeleteBookingAsync(Booking booking);
        Task<Booking> GetBookingAsync(int id);
        Task<IEnumerable<BookingDto>> GetBookingsAsync();
        Task<Booking> UpdateBookingAsynce( Booking booking);
        Task<IEnumerable<CinemaSeat>> getdata();
        Task<Booking> CreateBooking(BookingUser bookingUser);
    }
}