using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class BookingController : ApiControllerBase
    {

        private readonly IBookingService _bookingService;
        private readonly ILogger<BookingController> _logger;
        private readonly IMapper _mapper;

        public BookingController(IBookingService bookingService, ILogger<BookingController> logger, IMapper mapper)
        {
            _bookingService = bookingService;
            _logger = logger;
            _mapper = mapper;
        }


        

        
    }
        
}
