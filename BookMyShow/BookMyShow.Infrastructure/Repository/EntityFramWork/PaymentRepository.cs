using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;


        public PaymentRepository()
        {
            _bookMyShowContext = new BookMyShowContext();
        }


        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            return await (from payment in _bookMyShowContext.Payments

                          select new Payment
                          {
                              PaymentId = payment.PaymentId,
                              Amount = payment.Amount,
                              TimeStamp = payment.TimeStamp,
                              DicountCoupon = payment.DicountCoupon,
                              RemoteTransactionId = payment.RemoteTransactionId,
                              PeyementMethod = payment.PeyementMethod,
                              BookingId = payment.BookingId,
                          }).ToListAsync();

        }

        public async Task<Payment> GetPaymentAsync(int id)
        {
            return await _bookMyShowContext.Payments.FindAsync(id);
        }

        public async Task<Payment> AddPaymentAsync(Payment payment)
        {
            _bookMyShowContext.Payments.Add(payment);
            await _bookMyShowContext.SaveChangesAsync();
            return payment;
        }
        public async Task<Payment> UpdatePaymentAsynce(int id, Payment payment)
        {
            var paymentToBeUpdated = await GetPaymentAsync(id);
            paymentToBeUpdated.Amount = payment.Amount;
            paymentToBeUpdated.TimeStamp = payment.TimeStamp;
            paymentToBeUpdated.DicountCoupon = payment.DicountCoupon;
            paymentToBeUpdated.RemoteTransactionId = payment.RemoteTransactionId;
            paymentToBeUpdated.PeyementMethod = payment.PeyementMethod;
            paymentToBeUpdated.BookingId = payment.BookingId;
            _bookMyShowContext.Payments.Update(paymentToBeUpdated);
            await _bookMyShowContext.SaveChangesAsync();
            return paymentToBeUpdated;

        }

        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentAsync(id);
            _bookMyShowContext.Payments.Remove(payment);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
