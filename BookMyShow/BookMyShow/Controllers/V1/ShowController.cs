using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
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
    public class ShowController : ApiControllerBase
    {

        private readonly IShowRepository _showRepository;
        private readonly ILogger<ShowController> _logger;
        private readonly IMapper _mapper;
        public ShowController(IShowRepository showRepository, ILogger<ShowController> logger, IMapper mapper)
        {
            _showRepository = showRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ShowController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ShowDto>>> Get()
        {
            _logger.LogInformation("Getting list of all Shows");
            var result = await _showRepository.GetShowsAsync();
            return Ok(result);
        }

        // GET <ShowController>/5
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
            _logger.LogInformation("Getting Id : {id} Show", id);
            var ShowResult = await _showRepository.GetShowAsync(id);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <ShowController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] ShowVm showVm)
        {
            _logger.LogInformation("add new Show");
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var ShowResult = await _showRepository.AddShowAsync(show);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            return Ok(result);
        }

        // PUT <ShowController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] ShowVm showVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} Show", id);
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var ShowResult = await _showRepository.UpdateShowAsynce(id, show);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            return Ok(result);
        }

        // DELETE <ShowController>/5
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
            _logger.LogInformation("Deleted Id :  {id}  Show", id);
            await _showRepository.DeleteShowAsync(id);
        }
    }
}
