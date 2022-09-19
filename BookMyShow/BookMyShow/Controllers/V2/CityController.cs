using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CityController : ApiControllerBase
    {
        private readonly ICityService _cityService;
        private readonly ILogger<CityController> _logger;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, ILogger<CityController> logger, IMapper mapper)
        {
            _cityService = cityService;
            _logger = logger;
            _mapper = mapper;
        }
        [ApiVersion("2.0")]
        [Route("cityCinema")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityCinema(string cityCinema)
        {
            var result = await _cityService.GetCinemaCitysync(cityCinema);
            return Ok(result);

        }


        [ApiVersion("2.0")]
        [Route("{cityMovie}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityMovie(string cityMovie)
        {
            var result = await _cityService.GetMovieCity(cityMovie);
            return Ok(result);

        }


       

    }
}
