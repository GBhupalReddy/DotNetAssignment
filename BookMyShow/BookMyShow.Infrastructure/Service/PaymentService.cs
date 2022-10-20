using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Repository.EntityFramWork;
using NPOI.SS.Formula.Functions;
using System.Collections.Generic;
using System.Diagnostics;

namespace BookMyShow.Infrastructure.Service
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IShowRepository _showRepository;
        private readonly IShowSeatRepository _showSeatRepository;
        private readonly IBookingService _bookingservice;
        private readonly IBookingRepository _bookingRepository;
        public PaymentService(IPaymentRepository paymentRepository, IShowRepository showRepository, IShowSeatRepository showSeatRepository, IBookingService bookingservice, IBookingRepository bookingRepository)
        {
            _paymentRepository = paymentRepository;
            _showRepository = showRepository;
            _showSeatRepository = showSeatRepository;
            _bookingservice = bookingservice;
            _bookingRepository = bookingRepository;
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

            payment.TimeStamp = DateTime.UtcNow;

            var booking = await _bookingservice.GetBookingByIdAsync(payment.BookingId);

            var updateShow = await _paymentRepository.GetUpdateShow(payment.BookingId);

            int numberofSeat = 0;

            if (booking.SeatType == 1)
            {
                numberofSeat = updateShow.Firstclass;
            }
            else if (booking.SeatType == 2)
            {
                numberofSeat = updateShow.SecondClass;
            }
            else if (booking.SeatType == 3)
            {
                numberofSeat = updateShow.ThirdClass;
            }

            // check tickets are available are not

            if (numberofSeat >= booking.NumberOfSeats)
            {
                var price = await _bookingRepository.GetSeatPrice(booking.SeatType);
                payment.Amount = price * booking.NumberOfSeats;
                var paymentresult = await _paymentRepository.AddPaymentAsync(payment);
                if (paymentresult != null)
                {


                    int cinemaHallId = await _bookingRepository.GetcinemaHallIdAsync(booking.ShowId);

                    var cinemaSeats = await _paymentRepository.GetCinemaSeatAsync(cinemaHallId, booking.SeatType);

                    var showSeatsbyBookId = await _showSeatRepository.GetShowSeatsByShowId(booking.ShowId);

                    List<int> seatlist = new List<int>();

                    foreach (var item1 in cinemaSeats)
                    {
                        seatlist.Add(item1.CinemaSeatId);
                        foreach (var item2 in showSeatsbyBookId)
                        {
                            if (item1.CinemaSeatId == item2.CinemaSeatId)
                            {
                                seatlist.Remove(item1.CinemaSeatId);
                            }
                        }
                    }

                    for (int i = 0; i < booking.NumberOfSeats; i++)
                    {
                        var cinemaseaid = await _bookingRepository.GetCinemaSeatIdAsync(seatlist[i], cinemaHallId);
                        ShowSeat showSeat1 = new()
                        {
                            Status = 1,
                            Price = price,
                            CinemaSeatId = cinemaseaid,
                            ShowId = booking.ShowId,
                            BookingId = paymentresult.BookingId,
                        };

                        await _showSeatRepository.AddShowSeatAsync(showSeat1);
                    }

                    booking.Status = 1;

                    await _bookingservice.UpdateBookingAsynce(paymentresult.BookingId, booking);

                    if (booking.SeatType == 1)
                    {
                        updateShow.Firstclass = updateShow.Firstclass - booking.NumberOfSeats;
                    }
                    else if (booking.SeatType == 2)
                    {
                        updateShow.SecondClass = updateShow.SecondClass - booking.NumberOfSeats;
                    }
                    else if (booking.SeatType == 3)
                    {
                        updateShow.ThirdClass = updateShow.ThirdClass - booking.NumberOfSeats;
                    }


                    await _showRepository.UpdateShowAsynce(updateShow);

                    return paymentresult;

                }
            }

            return null;
        }
        // Update payment using id
        public async Task<Payment> UpdatePaymentAsynce(int id, Payment payment)
        {

            var paymentToBeUpdated = await GetPaymentByIdAsync(id);
            paymentToBeUpdated.TimeStamp = payment.TimeStamp;
            paymentToBeUpdated.DicountCoupon = payment.DicountCoupon;
            paymentToBeUpdated.PeyementMethod = payment.PeyementMethod;
            paymentToBeUpdated.BookingId = payment.BookingId;
            paymentToBeUpdated.RemoteTransactionId = payment.RemoteTransactionId;

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

            // change status in  Booking

            var booking = await _bookingservice.GetBookingByIdAsync(payment.BookingId);

            booking.Status = 0;

            await _bookingRepository.UpdateBookingAsynce(booking);

            // update show in AvailableSeats

            var updateShow = await _paymentRepository.GetUpdateShow(payment.BookingId);
            if (booking.SeatType == 1)
            {
                updateShow.Firstclass = updateShow.Firstclass - booking.NumberOfSeats;
            }
            else if (booking.SeatType == 2)
            {
                updateShow.SecondClass = updateShow.SecondClass - booking.NumberOfSeats;
            }
            else if (booking.SeatType == 3)
            {
                updateShow.ThirdClass = updateShow.ThirdClass - booking.NumberOfSeats;
            }


            await _showRepository.UpdateShowAsynce(updateShow);

            // deleted showseats 

            var showSeats = await _showSeatRepository.GetShowSeatsByBookinId(booking.BookingId);

            foreach (var showSeat in showSeats)
            {
                await _showSeatRepository.DeleteShowSeatAsync(showSeat);
            }


        }
        public async Task<Payment> GetPaymentByBookinId(int bookingId)
        {
            var PaymentByBookinId = await _paymentRepository.GetPaymentByBookinId(bookingId);
            return PaymentByBookinId;
        }
    }
}
