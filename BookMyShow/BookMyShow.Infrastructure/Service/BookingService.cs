using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Core.Exceptions;

namespace BookMyShow.Infrastructure.Service
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper _mapper;
        private readonly IShowSeatRepository _showSeatRepository;

        public BookingService( IBookingRepository bookingRepository, IMapper mapper, IShowSeatRepository showSeatRepository)
        {
            _bookingRepository = bookingRepository;
            _mapper = mapper;
            _showSeatRepository = showSeatRepository;
        }

        // Get All Bookings
        public async Task<IEnumerable<BookingDto>> GetBookingsAsync()
        {
            var bookings = await _bookingRepository.GetBookingsAsync();
            return bookings;
        }

        // Get  Booking using booking id
        public async Task<Booking> GetBookingByIdAsync(int id)
        {

            var booking = await _bookingRepository.GetBookingAsync(id);
            return booking;
        }

        // Add booking
        public async Task<Booking> AddBookingAsync(Booking booking)
        {
            var reusult = await _bookingRepository.AddBookingAsync(booking);
            return reusult;
        }

        // Update booking using id
        public async Task<Booking> UpdateBookingAsynce(int id, Booking booking)
        {
            var bookingToBeUpdated = await GetBookingByIdAsync(id);
            bookingToBeUpdated.NumberOfSeats = booking.NumberOfSeats;
            bookingToBeUpdated.Timestamp = booking.Timestamp;
            bookingToBeUpdated.Status = booking.Status;
            bookingToBeUpdated.UserId = booking.UserId;
            bookingToBeUpdated.ShowId = booking.ShowId;

            var reusult = await _bookingRepository.UpdateBookingAsynce(bookingToBeUpdated);
            return reusult;

        }

        //deleted booking using id
        public async Task DeleteBookingAsync(int id)
        {
            var booking = await GetBookingByIdAsync(id);
            await _bookingRepository.DeleteBookingAsync(booking);


        }

        public async Task<Booking> CreateBookingAsync(BookingUser bookingUser)
        {
            
             int seatType = bookingUser.SeatType;
             var seatNumbers = await _bookingRepository.GetCinemaSeatsAsync(seatType, bookingUser.ShowId);
             int cinemaHallId = await _bookingRepository.GetcinemaHallIdAsync(bookingUser.ShowId);

             List<int> thirdClass = new List<int> { 1, 2 };
             List<int> secondClass = new List<int> { 3, 4 };
             List<int> firstClass = new List<int> { 5, 6 };

             if (seatType == 1)
             {
                var price = await _bookingRepository.GetSeatPrice(seatType);
                 foreach (var ab in seatNumbers)
                 {
                         thirdClass.Remove((int)ab);
                 }
                 if(thirdClass.Count() >= bookingUser.NumberOfSeats)
                  {
                   var result =   await  CreateBookingShowSeatAsync(bookingUser, thirdClass, price, cinemaHallId);
                    return result;
                  }
                 
             }
              if (seatType == 2)
              {
                var price = await _bookingRepository.GetSeatPrice(seatType);
                foreach (var ab in seatNumbers)
                  {
                  secondClass.Remove((int)ab);
                  }
                  if (secondClass.Count() >= bookingUser.NumberOfSeats)
                  {
                    var result = await CreateBookingShowSeatAsync(bookingUser, secondClass, price, cinemaHallId);
                    return result;
                   }
               }
              if (seatType == 3)
              {
                var price = await _bookingRepository.GetSeatPrice(seatType);
                foreach (var ab in seatNumbers)
                  {
                      firstClass.Remove((int)ab);
                  }
                  if (firstClass.Count() >= bookingUser.NumberOfSeats)
                  {
                    var result =  await CreateBookingShowSeatAsync(bookingUser, firstClass, price, cinemaHallId);
                    return result;
                   }
               }
             
            return null;
        }
        public async Task<Booking> CreateBookingShowSeatAsync(BookingUser bookingUser,List<int> list,decimal price,int cinemaHallId)
        {
            var booking = _mapper.Map<BookingUser, Booking>(bookingUser);
            var data = await AddBookingAsync(booking);

            if (data != null)
            {
                for (int i = 0; i < bookingUser.NumberOfSeats; i++)
                {
                    var cinemaseaid = await _bookingRepository.GetCinemaSeatIdAsync(list[i], cinemaHallId);
                    ShowSeat showSeat1 = new()
                    {
                        Status = 1,
                        Price = price,
                        CinemaSeatId = cinemaseaid,
                        ShowId = bookingUser.ShowId,
                        BookingId = data.BookingId,
                    };

                    await _showSeatRepository.AddShowSeatAsync(showSeat1);
                }
            }
            return booking;
        }


        public Task<int> VerifyBookingExist(int id)
        {
            if (id <= 0)
            {
                throw id switch
                {
                    _ => new AppException("Input request is invalid")
                };
            }
            throw id switch
            {

                _ => new DuplicateException("Booking exits ", new Exception($"Booking not existed with this {id} Id"))
            };
        }
    }
}

