using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("city")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CityController : ApiControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<CityController> _logger;
        private readonly IMapper _mapper;

        public CityController(ICityService cityService, IExceptionService exceptionService, ILogger<CityController> logger, IMapper mapper)
        {
            _cityService = cityService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CityController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
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
        [HttpGet("{id}"), AllowAnonymous]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CityDto>> GetCity(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id {id} City", id);
            var city = await _cityService.GetCityByIdAsync(id);
            var result = _mapper.Map<City, CityDto>(city);
            if (result is null)
                await _exceptionService.VerifyIdExist(id,"City");
            return Ok(result);
        }

        // POST <CityController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CityDto>> PostCity([FromBody] CityVm cityVm)
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
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<CityDto>> PutCity(int id, [FromBody] CityVm cityVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} City", id);
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityService.UpdateCityAsynce(id, city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            if(result is null)
            {
                await _exceptionService.VerifyIdExist(id,"City");
            }
            return Ok(result);
        }


        // DELETE <CityController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteCity(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted  {id}  City", id);
            await _cityService.DeleteCityAsync(id);
        }
        [ApiVersion("1.0")]
        [Route("cinema/{cityName}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaDto>>> GetCinemaInCity(string cityName)
        {

            var result = await _cityService.GetCinemaInCityAsync(cityName);
            if (!result.Any())
            {
                return NotFound("data not found");
            }
            return Ok(result);

        }


        [ApiVersion("1.0")]
        [Route("movies/{cityName}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDto>>> GetMovieInCity(string cityName,string? language = null,string? genre = null)
        {
            var result = await _cityService.GetMovieInCity(cityName, language, genre);
            if (!result.Any())
            {
                return NotFound("data not found");
            }
            return Ok(result);

        }

        [ApiVersion("1.0")]
        [Route("movie-{cityName}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<MovieDetailes>>> GetCityCinemaMovie(string cityName, string? cinemaName = null)
        {
            var result = await _cityService.GetCityCinemaMovieAsync(cityName, cinemaName);
            if(!result.Any())
            {
                return NotFound("data not found");
            }
            return Ok(result);

        }


    }
}
