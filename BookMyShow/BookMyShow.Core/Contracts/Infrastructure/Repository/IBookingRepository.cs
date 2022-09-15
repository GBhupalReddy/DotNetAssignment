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
        Task<int> GetcinemaHallId(BookingUser bookingUser);
        Task<List<int?>> GetCinemaSeats(int seatType,BookingUser bookingUser);
        Task<IEnumerable<ShowSeat>> GetBookedTickets(decimal a, BookingUser bookingUser);
        Task<int> GetCinemaSeatId(int id, int cinemaHallId);
    }
}