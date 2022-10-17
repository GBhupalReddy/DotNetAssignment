using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaController : ApiControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly ILogger<CinemaController> _logger;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaService cinemaService, ILogger<CinemaController> logger, IMapper mapper)
        {
            _cinemaService = cinemaService;
            _logger = logger;
            _mapper = mapper;
        }


    }
}
