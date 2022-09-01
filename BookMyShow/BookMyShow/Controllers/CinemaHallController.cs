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
    public class CinemaHallController : ControllerBase
    {
        private readonly ICinemaHallRepository _cinemaHallRepository;
        private readonly IMapper _mapper;
        public CinemaHallController(ICinemaHallRepository cinemaHallRepository, IMapper mapper)
        {
            _cinemaHallRepository = cinemaHallRepository;
            _mapper = mapper;
        }

        // GET: api/<CinemaHallController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result= await _cinemaHallRepository.GetCinemaHallsAsync();
            return Ok(result);
        }

        // GET api/<CinemaHallController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _cinemaHallRepository.GetCinemaHallAsync(id);
            return Ok(result);
        }

        // POST api/<CinemaHallController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaHallVm cinemaHallVm)
        {
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var result = await _cinemaHallRepository.AddCinemaHallAsync(cinemaHall);
            return Ok(result);

        }

        // PUT api/<CinemaHallController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaHallVm cinemaHallVm)
        {
            var cinemaHall = _mapper.Map<CinemaHallVm, CinemaHall>(cinemaHallVm);
            var result = await _cinemaHallRepository.UpdateCinemaHallAsynce(id,cinemaHall);
            return Ok(result);

        }

        // DELETE api/<CinemaHallController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cinemaHallRepository.DeleteCinemaHallrAsync(id);
        }
    }
}
