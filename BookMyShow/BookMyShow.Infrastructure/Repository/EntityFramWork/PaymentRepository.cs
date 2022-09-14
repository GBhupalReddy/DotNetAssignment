using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        private readonly IMapper _mapper;
        public PaymentRepository(BookMyShowContext bookMyShowContext, IMapper mapper, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
            _mapper = mapper;
        }

        // Get all payments
        public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync()
        {
            var query = "select * from Payment";
            var result = await _dbConnection.QueryAsync<PaymentDto>(query);
            return result;

        }

        // Get payment using id
        public async Task<Payment> GetPaymentAsync(int id)
        {
            var query = "select * from Payment where PaymentId = @id";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Payment>(query, new { id }));
            return result;
        }

        // Add payment
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            var amount = from booking in _bookMyShowContext.ShowSeats
                         where booking.BookingId == payment.BookingId
                         select booking;

            payment.Amount = amount.Select(c=>c.Price).Sum();
            _bookMyShowContext.Payments.Add(payment);
            await _bookMyShowContext.SaveChangesAsync();
           if(payment != null)
            {
                var cinemaSeats = await (from showSeat in _bookMyShowContext.ShowSeats
                                         join cinemaSeat in _bookMyShowContext.CinemaSeats
                                         on showSeat.CinemaSeatId equals cinemaSeat.CinemaSeatId
                                         where showSeat.BookingId == payment.BookingId
                                         select cinemaSeat).ToListAsync();
                
                 int bokkedTikets = cinemaSeats.Select(c => c.CinemaSeatId).Count();
                var updateShow = await (from show in _bookMyShowContext.Shows
                                        join booking in _bookMyShowContext.Bookings
                                        on show.ShowId equals booking.ShowId
                                        where booking.BookingId == payment.BookingId
                                       select show).FirstAsync();
                updateShow.AvailableSeats = updateShow.AvailableSeats - bokkedTikets;
                  _bookMyShowContext.Shows.Update(updateShow);
                  await _bookMyShowContext.SaveChangesAsync();
            }
            return payment;
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce( Payment payment)
        {
           
            var amount = from booking in _bookMyShowContext.ShowSeats
                         where booking.BookingId == payment.BookingId
                         select booking;
            payment.Amount= amount.Select(c => c.Price).Sum();
            
            _bookMyShowContext.Payments.Update(payment);
            await _bookMyShowContext.SaveChangesAsync();
            return payment;

        }

        //deleted payment using id
        public async Task DeletePaymentAsync(Payment payment)
        {
            _bookMyShowContext.Payments.Remove(payment);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
