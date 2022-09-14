using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        public PaymentService(IPaymentRepository paymentRepository)
        {
            _paymentRepository = paymentRepository;
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
            var result = await _paymentRepository.AddPaymentAsync(payment);
            return result;
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
