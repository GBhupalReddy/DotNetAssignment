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
        Task<int> GetcinemaHallIdAsync(int showId);
        Task<IEnumerable<int>> GetCinemaSeatsAsync(int seatType,int bookingUser);
        Task<decimal> GetSeatPrice(int seatType);
        Task<int> GetCinemaSeatIdAsync(int id, int cinemaHallId);
    }
}