using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
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
    public class CinemaController : ApiControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly ILogger<CinemaController> _logger;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaService _cinemaService, ILogger<CinemaController> logger, IMapper mapper)
        {
            this._cinemaService = _cinemaService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CinemaController>
        [ApiVersion("1.0")]

        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaDto>>> GetCinemas()
        {
            _logger.LogInformation("Getting list of all Cinemas");
            var result = await _cinemaService.GetCinemasAsync();
            return Ok(result);
        }

        // GET <CinemaController>/
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCinema(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id {id} Cinema", id);
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            if (cinema is null)
                return NotFound("Please Enter Valid Data");
            var cinemaDto = _mapper.Map<Cinema, CinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        // POST <CinemaController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PostCinema([FromBody] CinemaVm cinemaVm)
        {
            _logger.LogInformation("add new Cinema");
            var cinema = _mapper.Map<CinemaVm, Cinema>(cinemaVm);
            var cinemaResult = await _cinemaService.AddCinemaAsync(cinema);
            var cinemaDto = _mapper.Map<Cinema, CinemaDto>(cinemaResult);
            return Ok(cinemaDto);
        }

        // PUT <CinemaController>
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> PutCinema(int id, [FromBody] CinemaVm cinemaVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Cinema", id);
            var cinema = _mapper.Map<CinemaVm, Cinema>(cinemaVm);
            var cinemaResult = await _cinemaService.UpdateCinemaAsynce(id, cinema);
            var result = _mapper.Map<Cinema, CinemaDto>(cinemaResult);
            return Ok(result);
        }

        // DELETE <CinemaController>
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteCinema(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted Id : {id}  Cinema", id);
            await _cinemaService.DeleteCinemaAsync(id);
        }
    }
}
