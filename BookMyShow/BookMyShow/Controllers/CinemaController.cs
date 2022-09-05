﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    
    public class CinemaController : ApiControllerBase
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly ILogger<CinemaController> _logger;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaRepository cinemaRepository, ILogger<CinemaController> logger, IMapper mapper)
        {
            _cinemaRepository = cinemaRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<CinemaController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all Cinemas");
            var result= await _cinemaRepository.GetCinemasAsync();
            return Ok(result);  
        }

        // GET api/<CinemaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Getting Id {id} Cinema",id);
            var result = await _cinemaRepository.GetCinemaAsync(id);
            return Ok(result);
        }

        // POST api/<CinemaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaVm cinemaVm)
        {
            _logger.LogInformation("add new Cinema");
            var cinema=_mapper.Map<CinemaVm,Cinema>(cinemaVm);
            var result=await _cinemaRepository.AddCinemaAsync(cinema);
            return Ok(result);
        }

        // PUT api/<CinemaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaVm cinemaVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}",id);
                return BadRequest();
            }
            _logger.LogInformation("Update Id: {id} Cinema",id);
            var cinema=_mapper.Map<CinemaVm,Cinema>(cinemaVm);
            var result = await _cinemaRepository.UpdateCinemaAsynce(id, cinema);
            return Ok(result);
        }

        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {

            _logger.LogInformation("Deleted Id : {id}  Cinema",id);
            await _cinemaRepository.DeleteCinemaAsync(id);  
        }
    }
}
