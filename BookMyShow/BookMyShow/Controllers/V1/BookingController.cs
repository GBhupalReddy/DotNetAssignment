using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("booking")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookingController : ApiControllerBase
    {

        private readonly IBookingService _bookingService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, IExceptionService exceptionService, ILogger<BookingController> logger, IMapper mapper)
        {
            _bookingService = bookingService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <BookingController>
        [MapToApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<BookingDto>>> GetBookings()
        {
            _logger.LogInformation("Getting list of all Bookings");
            var result = await _bookingService.GetBookingsAsync();
            return Ok(result);
        }

        // GET <BookingController>
        [MapToApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<BookingDto>> GetBookinfById(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }

            _logger.LogInformation("Getting Id : {id} Booking", id);
            var booking = await _bookingService.GetBookingByIdAsync(id);
            var result = _mapper.Map<Booking, BookingDto>(booking);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "Booking");
            }
            return Ok(result);
        }

        // POST <BookingController>
        [MapToApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<BookingDto>> Post([FromBody] BookingUserVm bookingUserVm)
        {
            var bookingUser = _mapper.Map<BookingUserVm, BookingUser>(bookingUserVm);
            var booking = await _bookingService.CreateBookingAsync(bookingUser);
            var result = _mapper.Map<Booking, BookingDto>(booking);
            if (result is null)
                return NotFound($"{bookingUser.NumberOfSeats} tickets are Not available ");
            return Ok(result);

        }

        // PUT <BookingController>
        [MapToApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<BookingDto>> PutBooking(int id, [FromBody] BookingVm bookingVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} Booking", id);
            var booking = _mapper.Map<BookingVm, Booking>(bookingVm);
            var bookingResult = await _bookingService.UpdateBookingAsynce(id, booking);
            var result = _mapper.Map<Booking, BookingDto>(bookingResult);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "Booking");
            }
            return Ok(result);
        }

        // DELETE <BookingController>
        [MapToApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteBooking(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);

            }
            _logger.LogInformation("Deleted Id :  {id}  Booking", id);
            await _bookingService.DeleteBookingAsync(id);

        }
    }
}
