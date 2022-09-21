using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShowRepository _showRepository;
        private readonly IShowSeatRepository _showSeatRepository;
        private readonly IBookingService _bookingservice;
        public PaymentService(IPaymentRepository paymentRepository, IShowRepository showRepository, IShowSeatRepository showSeatRepository, IBookingService bookingservice)
        {
            _paymentRepository = paymentRepository;
            _showRepository = showRepository;
            _showSeatRepository = showSeatRepository;
            _bookingservice = bookingservice;
        }
        // Get all payments
        public async Task<IEnumerable<PaymentDto>> GetPaymentsAsync()
        {
            var payments = await _paymentRepository.GetPaymentsAsync();
            return payments;
        }

        // Get payment using id
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetPaymentAsync(id);
            return payment;
        }

        // Add payment
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            var bookingAmount = await _paymentRepository.GetBookingAmount(payment.BookingId);

            payment.Amount = bookingAmount.BookingAmount;

            var paymentresult = await _paymentRepository.AddPaymentAsync(payment);

            if (paymentresult != null)
            {

                var updateShow = await _paymentRepository.GetUpdateShow(paymentresult.BookingId);

                updateShow.AvailableSeats = updateShow.AvailableSeats - bookingAmount.NoOfBookings;

                await _showRepository.UpdateShowAsynce(updateShow);

            }
            return paymentresult;
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce(int id, Payment payment)
        {
            
            var paymentToBeUpdated = await GetPaymentByIdAsync(id);
            paymentToBeUpdated.TimeStamp = payment.TimeStamp;
            paymentToBeUpdated.DicountCoupon=payment.DicountCoupon;
            paymentToBeUpdated.PeyementMethod=payment.PeyementMethod;
            paymentToBeUpdated.BookingId=payment.BookingId;
            paymentToBeUpdated.RemoteTransactionId=payment.RemoteTransactionId;

            var bookinagAmount = await _paymentRepository.GetBookingAmount(paymentToBeUpdated.BookingId);

            paymentToBeUpdated.Amount = bookinagAmount.BookingAmount;

            var result = await _paymentRepository.UpdatePaymentAsynce(paymentToBeUpdated);
            return result;
        }

        //deleted payment using id
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            await _paymentRepository.DeletePaymentAsync(payment);

            // update show in AvailableSeats

            var bookingAmount = await _paymentRepository.GetBookingAmount(payment.BookingId);

          //  int bokkedTikets = bookinagAmount.Select(c => c.CinemaSeatId).Count();

            var updateShow = await _paymentRepository.GetUpdateShow(payment.BookingId);
            updateShow.AvailableSeats = updateShow.AvailableSeats + bookingAmount.NoOfBookings;

            await _showRepository.UpdateShowAsynce(updateShow);

            // delete booking seats in show seats
             
             await  _paymentRepository.DeleteShowSeatAsync(payment.BookingId);

            // delete booking

            await _bookingservice.DeleteBookingAsync(payment.BookingId);

        }
        public  async Task<Payment> GetPaymentByBookinId(int bookingId)
        {
            var PaymentByBookinId = await _paymentRepository.GetPaymentByBookinId(bookingId);
            return PaymentByBookinId;
        }
    }
}
