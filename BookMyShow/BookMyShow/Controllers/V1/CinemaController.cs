using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
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
    [Route("cinema")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaController : ApiControllerBase
    {
        private readonly ICinemaService _cinemaService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<CinemaController> _logger;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaService cinemaService, IExceptionService exceptionService, ILogger<CinemaController> logger, IMapper mapper)
        {
            _cinemaService = cinemaService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CinemaController>
        [ApiVersion("1.0")]

        [Route(""), AllowAnonymous]
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
        [Route("{id}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CinemaDto>> GetCinema(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id {id} Cinema", id);
            var cinema = await _cinemaService.GetCinemaByIdAsync(id);
            var cinemaDto = _mapper.Map<Cinema, CinemaDto>(cinema);
            if (cinemaDto is null)
                await _exceptionService.VerifyIdExist(id, "Cinema");
            return Ok(cinemaDto);
        }

        // POST <CinemaController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CinemaDto>> PostCinema([FromBody] CinemaVm cinemaVm)
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
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<CinemaDto>> PutCinema(int id, [FromBody] CinemaVm cinemaVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} Cinema", id);
            var cinema = _mapper.Map<CinemaVm, Cinema>(cinemaVm);
            var cinemaResult = await _cinemaService.UpdateCinemaAsynce(id, cinema);
            var result = _mapper.Map<Cinema, CinemaDto>(cinemaResult);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "Cinema");
            }
            return Ok(result);
        }

        // DELETE <CinemaController>
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteCinema(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted Id : {id}  Cinema", id);
            await _cinemaService.DeleteCinemaAsync(id);
        }
    }
}
