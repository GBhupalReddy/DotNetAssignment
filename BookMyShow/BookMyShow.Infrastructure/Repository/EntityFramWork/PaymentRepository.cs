using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using System.Data;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;
        public PaymentRepository(BookMyShowContext bookMyShowContext , IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
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

        public async Task DeleteShowSeatAsync(int bookingId)
        {
            var DeleteShowSeatQuery = "Delete from ShowSeat where BookingId = @bookingId";
            await _dbConnection.QueryAsync(DeleteShowSeatQuery, new { bookingId });
        }

        public async Task<GetBookingAmount> GetBookingAmount(int bookingId)
        {
            var query = "execute GetBookingAmount @BookingId";
            var result = await _dbConnection.QueryFirstOrDefaultAsync<GetBookingAmount>(query, new { bookingId });

            return result;
        }
        
        public async Task<Show> GetUpdateShow(int bookiniId)
        {
            var updateShowQuery = "execute GetShowByBookigId @bookiniId";
            var updateShow = await _dbConnection.QueryFirstOrDefaultAsync<Show>(updateShowQuery, new { bookiniId });

            return updateShow;
        }
        public async Task<Payment> GetPaymentByBookinId(int bookingId)
        {
            var PaymentByBookinIdQuery = "Select * from Payment Where BookingId = @bookingId";

            var PaymentByBookinId = await _dbConnection.QueryFirstOrDefaultAsync<Payment>(PaymentByBookinIdQuery, new { bookingId});    

            return PaymentByBookinId;
        }

        public async Task<IEnumerable<CinemaSeat>> GetCinemaSeatAsync(int hallId,int seatType)
        {
            var Query = " select * from cinemaSeat where SeatType = @seatType and CinemaHallId= @HallId";
            var result = await _dbConnection.QueryAsync<CinemaSeat>(Query, new { seatType , hallId });
            return result;
        }

    }
}
