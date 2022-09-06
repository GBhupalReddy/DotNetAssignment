using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;

namespace BookMyShow.Core.Contracts.Infrastructure.Repository
{
    public interface IPaymentRepository
    {
        Task<Payment> AddPaymentAsync(Payment payment);
        Task DeletePaymentAsync(int id);
        Task<Payment> GetPaymentAsync(int id);
        Task<IEnumerable<PaymentDto>> GetPaymentsAsync();
        Task<Payment> UpdatePaymentAsynce(int id, Payment payment);
    }
}