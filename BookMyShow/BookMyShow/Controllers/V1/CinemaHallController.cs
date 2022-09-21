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

        // GET: <CinemaHallController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaHallDto>>> Get()
        {
            _logger.LogInformation("Getting list of all CinemaHalls");
            var result = await _cinemaHallService.GetCinemaHallsAsync();
            return Ok(result);
        }

        // GET <CinemaHallController>/5
        [ApiVersion("1.0")]
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
            _logger.LogInformation("Getting Id {id} CinemaHall", id);
            var cinemaHall = await _cinemaHallService.GetCinemaHallByIdAsync(id);
            var result = _mapper.Map<CinemaHall, CinemaHallDto>(cinemaHall);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <CinemaHallController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CinemaHallVm cinemaHallVm)
        {

            _logger.LogInformation("add new CinemaHall");
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var cinemaHallResult = await _cinemaHallService.AddCinemaHallAsync(cinemaHall);
            var result = _mapper.Map<CinemaHall, CinemaHallDto>(cinemaHallResult);
            return Ok(result);

        }

        // PUT <CinemaHallController>
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaHallVm cinemaHallVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} CinemaHall", id);
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var cinemaHallResult = await _cinemaHallService.UpdateCinemaHallAsynce(id, cinemaHall);
            var result = _mapper.Map<CinemaHall, CinemaHallDto>(cinemaHallResult);
            return Ok(result);

        }

        // DELETE <CinemaHallController>
        [ApiVersion("1.0")]
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
            _logger.LogInformation("Deleted  {id}  CinemaHall", id);
            await _cinemaHallService.DeleteCinemaHallrAsync(id);
        }
    }
}
