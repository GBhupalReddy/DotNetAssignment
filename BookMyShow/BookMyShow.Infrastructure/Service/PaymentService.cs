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
        public PaymentService(IPaymentRepository paymentRepository, IShowRepository showRepository)
        {
            _paymentRepository = paymentRepository;
            _showRepository = showRepository;
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
            var bookinagAmount = await _paymentRepository.GetBookingAmount(payment);

            payment.Amount = bookinagAmount.Select(c => c.Price).Sum();

            var paymentresult = await _paymentRepository.AddPaymentAsync(payment);

           if(paymentresult != null)
            {
                var cinemaSeats = await _paymentRepository.GetCinemaSeats(paymentresult);

                int bokkedTikets = cinemaSeats.Select(c => c.CinemaSeatId).Count();

                var updateShow = await _paymentRepository.GetUpdateShow(paymentresult);

                updateShow.AvailableSeats = updateShow.AvailableSeats - bokkedTikets;

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

            var bookinagAmount = await _paymentRepository.GetBookingAmount(paymentToBeUpdated);

            paymentToBeUpdated.Amount = bookinagAmount.Select(c => c.Price).Sum();

            var result = await _paymentRepository.UpdatePaymentAsynce(paymentToBeUpdated);
            return result;
        }

        //deleted payment using id
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            await _paymentRepository.DeletePaymentAsync(payment);
        }
    }
}
