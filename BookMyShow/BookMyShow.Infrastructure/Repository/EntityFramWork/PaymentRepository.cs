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
                var cinemaHall = await (from showSeat in _bookMyShowContext.ShowSeats
                                           join cinemaSeat in _bookMyShowContext.CinemaSeats
                                           on showSeat.CinemaSeatId equals cinemaSeat.CinemaSeatId
                                           join cinema in _bookMyShowContext.CinemaHalls
                                           on cinemaSeat.CinemaHallId equals cinema.CinemaHallId
                                           where showSeat.BookingId == 2
                                           select new CinemaHall
                                           {
                                               CinemaHallId = cinema.CinemaHallId,
                                               CinemaHallName = cinema.CinemaHallName,
                                               AvailableSeats = cinema.AvailableSeats,
                                               CinemaId = cinema.CinemaId,
                                               TotalSeats = cinema.TotalSeats
                                           }).FirstOrDefaultAsync();
                cinemaHall.AvailableSeats = cinemaHall.AvailableSeats - bokkedTikets;
                  _bookMyShowContext.CinemaHalls.Update(cinemaHall);
                  await _bookMyShowContext.SaveChangesAsync();
            }
            return payment;
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce(int id, Payment payment)
        {
            var paymentToBeUpdated = await GetPaymentAsync(id);
            var amount = from booking in _bookMyShowContext.ShowSeats
                         where booking.BookingId == payment.BookingId
                         select booking;
            payment.Amount= amount.Select(c => c.Price).Sum();
            paymentToBeUpdated.TimeStamp = payment.TimeStamp;
            paymentToBeUpdated.DicountCoupon = payment.DicountCoupon;
            paymentToBeUpdated.RemoteTransactionId = payment.RemoteTransactionId;
            paymentToBeUpdated.PeyementMethod = payment.PeyementMethod;
            paymentToBeUpdated.BookingId = payment.BookingId;
            _bookMyShowContext.Payments.Update(paymentToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return paymentToBeUpdated;

        }

        //deleted payment using id
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentAsync(id);
            _bookMyShowContext.Payments.Remove(payment);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
