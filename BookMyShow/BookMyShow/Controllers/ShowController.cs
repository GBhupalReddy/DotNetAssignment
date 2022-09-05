using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
   
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
        // GET: api/<ShowController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all Shows");
            var result = await _showRepository.GetShowsAsync();
            return Ok(result);
        }

        // GET api/<ShowController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id : {id} Show", id);
            var result = await _showRepository.GetShowAsync(id);
            return Ok(result);
        }

        // POST api/<ShowController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShowVm showVm)
        {
            _logger.LogInformation("add new Show");
            var show=_mapper.Map<ShowVm,Show>(showVm);
            var result = await _showRepository.AddShowAsync(show);
            return Ok(result);
        }

        // PUT api/<ShowController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShowVm showVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Show", id);
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var result = await _showRepository.UpdateShowAsynce(id,show);
            return Ok(result);
        }

        // DELETE api/<ShowController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
               
            }
            _logger.LogInformation("Deleted Id :  {id}  Show", id);
            await _showRepository.DeleteShowAsync(id);
        }
    }
}
