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
            return await _paymentRepository.GetPaymentsAsync();
        }

        // Get payment using id
        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _paymentRepository.GetPaymentAsync(id);
        }

        // Add payment
        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            return await _paymentRepository.AddPaymentAsync(payment);
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce(int id, Payment payment)
        {
            var paymentToBeUpdated = await GetPaymentByIdAsync(id);

            return await _paymentRepository.UpdatePaymentAsynce(paymentToBeUpdated);
        }

        //deleted payment using id
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentByIdAsync(id);
            await _paymentRepository.DeletePaymentAsync(payment);
        }
    }
}
