using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
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

        // GET: api/<BookingController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all Bookings");
            var Result= await _bookingRepository.GetBookingsAsync();
            return Ok(Result);
        }

        // GET api/<BookingController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }

            _logger.LogInformation("Getting Id : {id} Booking", id);
            var Result = await _bookingRepository.GetBookingAsync(id);
            return Ok(Result);
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] BookingVm bookingVm)
        {

            _logger.LogInformation("add new Booking");
            var booking=_mapper.Map<BookingVm,Booking>(bookingVm);
            var Result = await _bookingRepository.AddBookingAsync(booking);
            return Ok(Result);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] BookingVm bookingVm)
        {
            if (id <= 0 )
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Booking", id);
            var booking = _mapper.Map<BookingVm, Booking>(bookingVm);
            var Result = await _bookingRepository.UpdateBookingAsynce(id,booking);
            return Ok(Result);
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                
            }
            _logger.LogInformation("Deleted Id :  {id}  Booking", id);
            await _bookingRepository.DeleteBookingAsync(id);
            
        }
    }
}
