﻿using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Data;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMyShow.Infrastructure.Repository.EntityFramWork
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly BookMyShowContext _bookMyShowContext;
        private readonly IDbConnection _dbConnection;


        public PaymentRepository(BookMyShowContext bookMyShowContext, IDbConnection dbConnection)
        {
            _bookMyShowContext = bookMyShowContext;
            _dbConnection = dbConnection;
        }

        // Get all payments
        public async Task<IEnumerable<Payment>> GetPaymentsAsync()
        {
            var query = "select * from Payment";
            var result = await _dbConnection.QueryAsync<Payment>(query);
            return result;

        }

        // Get payment using id
        public async Task<Payment> GetPaymentAsync(int id)
        {
            var query = "select * from Payment where PaymentId = @id";
            var result = await _dbConnection.QueryFirstAsync<Payment>(query, new { id = id });
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

        //deleted payment using id
        public async Task DeletePaymentAsync(int id)
        {
            var payment = await GetPaymentAsync(id);
            _bookMyShowContext.Payments.Remove(payment);
            await _bookMyShowContext.SaveChangesAsync();
        }
    }
}
