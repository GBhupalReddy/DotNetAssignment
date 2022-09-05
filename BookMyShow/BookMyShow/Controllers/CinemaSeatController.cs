﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
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

        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all CinemaSeats");
            var result=await _cinemaSeatRepository.GetCinemaSeatsAsync();
            return Ok(result);
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} CinemaSeat",id);
            var result = await _cinemaSeatRepository.GetCinemaSeatAsync(id);
            return Ok(result);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaSeatVm cinemaSeatVm)
        {
            _logger.LogInformation("add new CinemaSeat");
            var cinemaSeat=_mapper.Map<CinemaSeatVm,CinemaSeat>(cinemaSeatVm);
            var result=await _cinemaSeatRepository.AddCinemaSeatAsync(cinemaSeat);
            return Ok(result);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaSeatVm cinemaSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} CinemaSeat",id);
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var result = await _cinemaSeatRepository.UpdateCinemaSeatAsynce(id,cinemaSeat);
            return Ok(result);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
               
            }
            _logger.LogInformation("Deleted  {id}  CinemaSeat",id);
            await _cinemaSeatRepository.DeleteCinemaSeatAsync(id);
        }
    }
}
