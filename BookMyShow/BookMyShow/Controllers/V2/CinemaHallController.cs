using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaHallController : ApiControllerBase
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly ILogger<CinemaHallController> _logger;
        private readonly IMapper _mapper;
        public CinemaHallController(ICinemaHallService cinemaHallService, ILogger<CinemaHallController> logger, IMapper mapper)
        {
            _cinemaHallService = cinemaHallService;
            _logger = logger;
            _mapper = mapper;
        }


    }
}
