using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("cinemahall")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaHallController : ApiControllerBase
    {
        private readonly ICinemaHallService _cinemaHallService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<CinemaHallController> _logger;
        private readonly IMapper _mapper;
        public CinemaHallController(ICinemaHallService cinemaHallService, IExceptionService exceptionService, ILogger<CinemaHallController> logger, IMapper mapper)
        {
            _cinemaHallService = cinemaHallService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <CinemaHallController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaHallDto>>> GetCinemaHalls()
        {
            _logger.LogInformation("Getting list of all CinemaHalls");
            var result = await _cinemaHallService.GetCinemaHallsAsync();
            return Ok(result);
        }

        // GET <CinemaHallController>/5
        [ApiVersion("1.0")]
        [Route("{id}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<CinemaHallDto>> GetGetCinemaHall(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id {id} CinemaHall", id);
            var cinemaHall = await _cinemaHallService.GetCinemaHallByIdAsync(id);
            var result = _mapper.Map<CinemaHall, CinemaHallDto>(cinemaHall);
            if (result is null)
                await _exceptionService.VerifyIdExist(id, "CinemaHall");
            return Ok(result);
        }

        // POST <CinemaHallController>
        [ApiVersion("1.0")]
        [HttpPost]
        [Route("admin")]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<CinemaHallDto>> PostGetCinemaHall([FromBody] CinemaHallVm cinemaHallVm)
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
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<CinemaHallDto>> PutGetCinemaHall(int id, [FromBody] CinemaHallVm cinemaHallVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} CinemaHall", id);
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var cinemaHallResult = await _cinemaHallService.UpdateCinemaHallAsynce(id, cinemaHall);
            var result = _mapper.Map<CinemaHall, CinemaHallDto>(cinemaHallResult);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "CinemaHall");
            }
            return Ok(result);

        }

        // DELETE <CinemaHallController>
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteGetCinemaHall(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted  {id}  CinemaHall", id);
            await _cinemaHallService.DeleteCinemaHallrAsync(id);
        }
    }
}
