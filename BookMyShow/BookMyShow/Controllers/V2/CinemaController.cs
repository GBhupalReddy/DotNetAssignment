using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
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
    public class CinemaController : ApiControllerBase
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ILogger<CinemaController> _logger;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaRepository cinemaRepository, ILogger<CinemaController> logger, IMapper mapper)
        {
            _cinemaRepository = cinemaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CinemaController>
        [ApiVersion("2.0")]

        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Cinemas");
            var result = await _cinemaRepository.GetCinemasAsync();
            return Ok(result);
        }

        // GET <CinemaController>/
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id {id} Cinema", id);
            var cinema = await _cinemaRepository.GetCinemaAsync(id);
            var result = _mapper.Map<Cinema, CinemaDto>(cinema);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <CinemaController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CinemaVm cinemaVm)
        {
            _logger.LogInformation("add new Cinema");
            var cinema = _mapper.Map<CinemaVm, Cinema>(cinemaVm);
            var cinemaResult = await _cinemaRepository.AddCinemaAsync(cinema);
            var result = _mapper.Map<Cinema, CinemaDto>(cinemaResult);
            return Ok(result);
        }

        // PUT <CinemaController>
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaVm cinemaVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Cinema", id);
            var cinema = _mapper.Map<CinemaVm, Cinema>(cinemaVm);
            var cinemaResult = await _cinemaRepository.UpdateCinemaAsynce(id, cinema);
            var result = _mapper.Map<Cinema, CinemaDto>(cinemaResult);
            return Ok(result);
        }

        // DELETE <CinemaController>
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
            _logger.LogInformation("Deleted Id : {id}  Cinema", id);
            await _cinemaRepository.DeleteCinemaAsync(id);
        }
    }
}
