using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookingController : ApiControllerBase
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;

        public BookingController(IBookingRepository bookingRepository, ILogger<BookingController> logger, IMapper mapper)
        {
            _bookingRepository = bookingRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <BookingController>

        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<BookingDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Bookings");
            var result= await _bookingRepository.GetBookingsAsync();
            return Ok(result);
        }

        // GET <BookingController>

        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }

            _logger.LogInformation("Getting Id : {id} Booking", id);
            var booking = await _bookingRepository.GetBookingAsync(id);
            var result = _mapper.Map<Booking, BookingDto>(booking);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <BookingController>

        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] BookingVm bookingVm)
        {

            _logger.LogInformation("add new Booking");
            var booking=_mapper.Map<BookingVm,Booking>(bookingVm);
            var bookingResult = await _bookingRepository.AddBookingAsync(booking);
            var result = _mapper.Map<Booking, BookingDto>(bookingResult);
            return Ok(result);
        }

        // PUT <BookingController>

        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] BookingVm bookingVm)
        {
            if (id <= 0 )
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Booking", id);
            var booking = _mapper.Map<BookingVm, Booking>(bookingVm);
            var bookingResult = await _bookingRepository.UpdateBookingAsynce(id,booking);
            var result = _mapper.Map<Booking, BookingDto>(bookingResult);
            return Ok(result);
        }

        // DELETE <BookingController>

        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");

            }
            _logger.LogInformation("Deleted Id :  {id}  Booking", id);
            await _bookingRepository.DeleteBookingAsync(id);
            
        }
    }
}
