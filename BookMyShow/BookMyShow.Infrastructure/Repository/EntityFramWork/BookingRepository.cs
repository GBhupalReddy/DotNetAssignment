using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class BookingRepository : IBookingRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        public BookingRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<Booking>> GetBookingsAsync()
        {
            return await (from booking in _bookMyShowContext.Bookings

                          select new Booking
                          {
                              BookingId = booking.BookingId,
                              NumberOfSeats = booking.NumberOfSeats,
                              Timestamp = booking.Timestamp,
                              Status = booking.Status,
                              UserId = booking.UserId,
                              ShowId = booking.ShowId,
                          }).ToListAsync();

        }

        public async Task<Booking> GetBookingAsync(int id)
        {
            return await _bookMyShowContext.Bookings.FindAsync(id);
        }

        public async Task<Booking> AddBookingAsync(Booking user)
        {
            _bookMyShowContext.Bookings.Add(user);
            await _bookMyShowContext.SaveChangesAsync();
            return user;
        }
        public async Task<Booking> UpdateBookingAsynce(int id, Booking booking)
        {
            var bookingToBeUpdated = await GetBookingAsync(id);
            bookingToBeUpdated.NumberOfSeats = booking.NumberOfSeats;
            bookingToBeUpdated.Timestamp = booking.Timestamp;
            bookingToBeUpdated.Status = booking.Status;
            bookingToBeUpdated.UserId = booking.UserId;
            bookingToBeUpdated.ShowId = booking.ShowId;
            _bookMyShowContext.Bookings.Update(bookingToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return bookingToBeUpdated;

        }

        public async Task DeleteBookingAsync(int id)
        {
            var booking = await GetBookingAsync(id);
            _bookMyShowContext.Bookings.Remove(booking);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
