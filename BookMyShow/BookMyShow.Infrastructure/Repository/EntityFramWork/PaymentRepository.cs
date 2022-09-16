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
            var query = "execute GetPayments";
            var result = await _dbConnection.QueryAsync<PaymentDto>(query);
            return result;

        }

        // Get payment using id
        public async Task<Payment> GetPaymentAsync(int id)
        {
            var query = "execute GetPaymentById";
            var result = (await _dbConnection.QueryFirstOrDefaultAsync<Payment>(query, new { id }));
            return result;
        }

        // Add payment
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            
            _bookMyShowContext.Payments.Add(payment);
            await _bookMyShowContext.SaveChangesAsync();
          
            return payment;
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce( Payment payment)
        {
            
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


        public async Task<IEnumerable<ShowSeat>> GetBookingAmount(Payment payment)
        {
            var query = "execute GetBookingAmount";
            var result = await _dbConnection.QueryAsync<ShowSeat>(query, new { payment.BookingId });

            //var amount =await (from booking in _bookMyShowContext.ShowSeats
            //             where booking.BookingId == payment.BookingId
            //             select booking).ToListAsync();

            return result;
        }
        public async Task<IEnumerable<CinemaSeat>> GetCinemaSeats(Payment payment)
        {
            var query = "execute GetBookedCinemaSeat";
            var result = await _dbConnection.QueryAsync<CinemaSeat>(query, new { payment.BookingId });

            //var cinemaSeats = await (from showSeat in _bookMyShowContext.ShowSeats
            //                         join cinemaSeat in _bookMyShowContext.CinemaSeats
            //                         on showSeat.CinemaSeatId equals cinemaSeat.CinemaSeatId
            //                         where showSeat.BookingId == payment.BookingId
            //                         select cinemaSeat).ToListAsync();

            return result;
        }
        public async Task<Show> GetUpdateShow(Payment payment)
        {
            var updateShow = await (from show in _bookMyShowContext.Shows
                                    join booking in _bookMyShowContext.Bookings
                                    on show.ShowId equals booking.ShowId
                                    where booking.BookingId == payment.BookingId
                                    select show).FirstAsync();

            return updateShow;
        }

    }
}
