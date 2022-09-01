﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICinemaRepository _cinemaRepository;
        private readonly IMapper _mapper;
        public CinemaController(ICinemaRepository cinemaRepository, IMapper mapper)
        {
            _cinemaRepository = cinemaRepository;
            _mapper = mapper;
        }

        // GET: api/<CinemaController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var result= await _cinemaRepository.GetCinemasAsync();
            return Ok(result);  
        }

        // GET api/<CinemaController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var result = await _cinemaRepository.GetCinemaAsync(id);
            return Ok(result);
        }

        // POST api/<CinemaController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CinemaVm cinemaVm)
        {
            var cinema=_mapper.Map<CinemaVm,Cinema>(cinemaVm);
            var result=await _cinemaRepository.AddCinemaAsync(cinema);
            return Ok(result);
        }

        // PUT api/<CinemaController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaVm cinemaVm)
        {
            var cinema=_mapper.Map<CinemaVm,Cinema>(cinemaVm);
            var result = await _cinemaRepository.UpdateCinemaAsynce(id, cinema);
            return Ok(result);
        }

        // DELETE api/<CinemaController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _cinemaRepository.DeleteCinemaAsync(id);  
        }
    }
}
