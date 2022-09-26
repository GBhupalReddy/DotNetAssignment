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
    [Route("cinemaseat")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaSeatController : ApiControllerBase
    {

        private readonly ICinemaSeatService _cinemaSeatService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<CinemaSeatController> _logger;
        private readonly IMapper _mapper;
        public CinemaSeatController(ICinemaSeatService cinemaSeatService,IExceptionService exceptionService, ILogger<CinemaSeatController> logger, IMapper mapper)
        {

            _cinemaSeatService = cinemaSeatService;
            _exceptionService = exceptionService;   
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ValuesController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaSeatDto>>> GetCinemaSeats()
        {
            _logger.LogInformation("Getting list of all CinemaSeats");
            var result = await _cinemaSeatService.GetCinemaSeatsAsync();
            return Ok(result);
        }

        // GET <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> GetCinemaSeat(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id {id} CinemaSeat", id);
            var cinemaSeat = await _cinemaSeatService.GetCinemaSeatByIdAsync(id);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "Cinema Seat");
            }
            return Ok(result);
        }

        // POST <ValuesController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> PostCinemaSeat([FromBody] CinemaSeatVm cinemaSeatVm)
        {
            _logger.LogInformation("add new CinemaSeat");
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatService.AddCinemaSeatAsync(cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            return Ok(result);
        }

        // PUT <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> PutCinemaSeat(int id, [FromBody] CinemaSeatVm cinemaSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} CinemaSeat", id);
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatService.UpdateCinemaSeatAsynce(id, cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            if(result is null)
            {
                await _exceptionService.VerifyIdExist(id,"Cinema Seat");
            }
            return Ok(result);
        }

        // DELETE <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteCinemaSeat(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted  {id}  CinemaSeat", id);
            await _cinemaSeatService.DeleteCinemaSeatAsync(id);
        }
    }
}
