using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
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

        // GET: <CityController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CityDto>>> GetCities()
        {
            _logger.LogInformation("Getting list of all Cities");
            var result = await _cityService.GetCitysAsync();
            return Ok(result);
        }

        //GET<CityController>/
        [ApiVersion("1.0")]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCity(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id {id} City", id);
            var city = await _cityService.GetCityByIdAsync(id);
            var result = _mapper.Map<City, CityDto>(city);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <CityController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PostCity([FromBody] CityVm cityVm)
        {
            _logger.LogInformation("add new City");
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityService.AddCityAsync(city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }


        // PUT <CityController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> PutCity(int id, [FromBody] CityVm cityVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} City", id);
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityService.UpdateCityAsynce(id, city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }


        // DELETE <CityController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteCity(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted  {id}  City", id);
            await _cityService.DeleteCityAsync(id);
        }
        [ApiVersion("1.0")]
        [Route("cinema/{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCinemaInCity(string cityName)
        {

            var result = await _cityService.GetCinemaInCityAsync(cityName);
            return Ok(result);

        }


        [ApiVersion("1.0")]
        [Route("movies/{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetMovieInCity(string cityName)
        {
            var result = await _cityService.GetMovieInCity(cityName);
            return Ok(result);

        }

        [ApiVersion("1.0")]
        [Route("movie-{cityName}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCityCinemaMovie(string cityName, string? cinemaName = null)
        {
            var result = await _cityService.GetCityCinemaMovieAsync(cityName, cinemaName);
            return Ok(result);

        }


    }
}
