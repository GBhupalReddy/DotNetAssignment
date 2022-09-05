using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
    public class CinemaHallController : ApiControllerBase
    {
        private readonly ICinemaHallRepository _cinemaHallRepository;
        private readonly ILogger<CinemaHallController> _logger;
        private readonly IMapper _mapper;
        public CinemaHallController(ICinemaHallRepository cinemaHallRepository, ILogger<CinemaHallController> logger, IMapper mapper)
        {
            _cinemaHallRepository = cinemaHallRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<CinemaHallController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all CinemaHalls");
            var result= await _cinemaHallRepository.GetCinemaHallsAsync();
            return Ok(result);
        }

        // GET api/<CinemaHallController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} CinemaHall",id);
            var result = await _cinemaHallRepository.GetCinemaHallAsync(id);
            return Ok(result);
        }

        // POST api/<CinemaHallController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaHallVm cinemaHallVm)
        {
            
            _logger.LogInformation("add new CinemaHall");
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var result = await _cinemaHallRepository.AddCinemaHallAsync(cinemaHall);
            return Ok(result);

        }

        // PUT api/<CinemaHallController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaHallVm cinemaHallVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} CinemaHall",id);
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var result = await _cinemaHallRepository.UpdateCinemaHallAsynce(id,cinemaHall);
            return Ok(result);

        }

        // DELETE api/<CinemaHallController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
               
            }
            _logger.LogInformation("Deleted  {id}  CinemaHall",id);
            await _cinemaHallRepository.DeleteCinemaHallrAsync(id);
        }
    }
}
