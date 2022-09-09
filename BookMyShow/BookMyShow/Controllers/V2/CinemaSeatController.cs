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
    public class CinemaSeatController : ApiControllerBase
    {

        private readonly ICinemaSeatRepository _cinemaSeatRepository;
        private readonly ILogger<CinemaSeatController> _logger;
        private readonly IMapper _mapper;
        public CinemaSeatController(ICinemaSeatRepository cinemaSeatRepository, ILogger<CinemaSeatController> logger, IMapper mapper)
        {

            _cinemaSeatRepository = cinemaSeatRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ValuesController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaSeatDto>>> Get()
        {
            _logger.LogInformation("Getting list of all CinemaSeats");
            var result = await _cinemaSeatRepository.GetCinemaSeatsAsync();
            return Ok(result);
        }

        // GET <ValuesController>/5
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
            _logger.LogInformation("Getting Id {id} CinemaSeat", id);
            var cinemaSeat = await _cinemaSeatRepository.GetCinemaSeatAsync(id);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <ValuesController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CinemaSeatVm cinemaSeatVm)
        {
            _logger.LogInformation("add new CinemaSeat");
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            return Ok(result);
        }

        // PUT <ValuesController>/5
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaSeatVm cinemaSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} CinemaSeat", id);
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatRepository.UpdateCinemaSeatAsynce(id, cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            return Ok(result);
        }

        // DELETE <ValuesController>/5
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
            _logger.LogInformation("Deleted  {id}  CinemaSeat", id);
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(id);
        }
    }
}
