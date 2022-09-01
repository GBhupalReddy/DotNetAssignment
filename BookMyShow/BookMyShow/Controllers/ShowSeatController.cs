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
    public class ShowSeatController : ControllerBase
    {

        private readonly IShowseatRepository _showseatRepository;
        private readonly IMapper _mapper;
        public ShowSeatController(IShowseatRepository showseatRepository, IMapper mapper)
        {
            _showseatRepository = showseatRepository;
            _mapper = mapper;
        }

        // GET: api/<ShoeSeatController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result = await _showseatRepository.GetShowSeatsAsync();
            return Ok(result);
        }

        // GET api/<ShoeSeatController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _showseatRepository.GetShowSaetAsync(id);
            return Ok(result);
        }

        // POST api/<ShoeSeatController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] ShowSeatVm showSeatVm)
        {
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var result = await _showseatRepository.AddShowSeatAsync(showSeat);
            return Ok(result);
        }

        // PUT api/<ShoeSeatController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] ShowSeatVm showSeatVm)
        {
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var result = await _showseatRepository.UpdateShowSeatAsynce(id,showSeat);
            return Ok(result);
        }

        // DELETE api/<ShoeSeatController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _showseatRepository.DeleteShowSeatAsync(id);
        }
    }
}
