using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ShowController : ControllerBase
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
            _logger.LogInformation($"Getting Id : {id} Show");
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
            _logger.LogInformation($"Update Id: {id} Show");
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var result = await _showRepository.UpdateShowAsynce(id,show);
            return Ok(result);
        }

        // DELETE api/<ShowController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleted Id :  {id}  Show");
            await _showRepository.DeleteShowAsync(id);
        }
    }
}
