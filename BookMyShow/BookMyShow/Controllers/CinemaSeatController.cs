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
    public class CinemaSeatController : ControllerBase
    {

        private readonly ICinemaSeatRepository _cinemaSeatRepository;
        private readonly IMapper _mapper;
        public CinemaSeatController(ICinemaSeatRepository cinemaSeatRepository, IMapper mapper)
        {
            _cinemaSeatRepository = cinemaSeatRepository;
            _mapper = mapper;
        }

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result=await _cinemaSeatRepository.GetCinemaSeatsAsync();
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _cinemaSeatRepository.GetCinemaSeatAsync(id);
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaSeatVm cinemaSeatVm)
        {
            var cinemaSeat=_mapper.Map<CinemaSeatVm,CinemaSeat>(cinemaSeatVm);
            var result=await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
            return Ok(result);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaSeatVm cinemaSeatVm)
        {
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var result = await _cinemaSeatRepository.UpdateCinemaSeatAsynce(id,cinemaSeat);
            return Ok(result);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(id);
        }
    }
}
