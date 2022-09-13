using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Service
{
    public interface IPaymentService
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsAsync();
        Task<Payment> UpdatePaymentAsynce(int id, Payment payment);
    }
}