using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
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

        [MapToApiVersion("2.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get()
        {
            //_logger.LogInformation("Getting list of all Bookings");
            //var result = await _bookingRepository.GetBookingsAsync();
            //return Ok(result);
            var data = await _bookingRepository.getdata();
             return Ok(data);
        }


        // POST <BookingController>
        [MapToApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<Booking>> Post([FromBody] BookingUser bokkingUser)
        {
         var data=  await _bookingRepository.CreateBooking(bokkingUser);
          return Ok(data);
            
        }

        
    }
        
}
