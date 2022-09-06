using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CityController : ApiControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CityController> _logger;
        private readonly IMapper _mapper;
        public CityController(ICityRepository cityRepository, ILogger<CityController> logger, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CityController>
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CityDto>>> Get()
        {
            _logger.LogInformation("Getting list of all City's");
            var result = await _cityRepository.GetCitysAsync();
            return Ok(result);
        }

        // GET <CityController>/5
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} City",id);
            var city= await _cityRepository.GetCityAsync(id);
            var result = _mapper.Map<City,CityDto>(city);
            if (result is null)
                return NotFound();
            return Ok(result);  
        }

        // POST <CityController>
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CityVm cityVm)
        {
            _logger.LogInformation("add new City");
            var city=_mapper.Map<CityVm,City>(cityVm);
            var cityResult = await _cityRepository.AddCityAsync(city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }

        // PUT <CityController>/5
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CityVm cityVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} City",id);
            var city = _mapper.Map<CityVm, City>(cityVm);
            var cityResult = await _cityRepository.UpdateCityAsynce(id,city);
            var result = _mapper.Map<City, CityDto>(cityResult);
            return Ok(result);
        }

        // DELETE <CityController>/5
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                
            }
            _logger.LogInformation("Deleted  {id}  City",id);
            await _cityRepository.DeleteCityAsync(id);
        }
    }
}
