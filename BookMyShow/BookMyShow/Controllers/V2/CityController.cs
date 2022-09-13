using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
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

        // GET: <CityController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            _logger.LogInformation("Getting list of all City's");
            var result = await _cityService.GetCitysAsync();
            return Ok(result);
        }

        //GET<CityController>/
        [ApiVersion("2.0")]
        [HttpGet("{id}")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id {id} City", id);
            var city = await _cityService.GetCityByIdAsync(id);
            var result = _mapper.Map<City, CityDto>(city);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        [ApiVersion("2.0")]
        [Route("cityMovie")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(string cityMovie)
        {
            var result = await _cityService.GetMovieCity(cityMovie);
            return Ok(result);

        }


        // POST <CityController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CityVm cityVm)
        {
            _logger.LogInformation("add new City");
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityService.AddCityAsync(city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }


        // PUT <CityController>/5
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CityVm cityVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} City", id);
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityService.UpdateCityAsynce(id, city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }


        // DELETE <CityController>/5
        [ApiVersion("2.0")]
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
            _logger.LogInformation("Deleted  {id}  City", id);
            await _cityService.DeleteCityAsync(id);
        }

    }
}
