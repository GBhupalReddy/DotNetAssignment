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
        [Route("CityCinema/{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityCinema(string cityName)
        {
            var result = await _cityService.GetCinemaCitysync(cityName);
            return Ok(result);

        }


        [ApiVersion("2.0")]
        [Route("CityMovies/{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityMovie(string cityName)
        {
            var result = await _cityService.GetCityMovie(cityName);
            return Ok(result);

        }

        [ApiVersion("2.0")]
        [Route("CityCinemaMovie/{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityCinemaMovie(string cityName, string? cinemaName = null)
        {
            var result = await _cityService.GetCityCinemaMovieAsync(cityName, cinemaName);
            return Ok(result);

        }



    }
}
