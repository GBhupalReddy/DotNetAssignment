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
    public class CityController : ControllerBase
    {
        private readonly ICityRepository _cityRepository;
        private readonly ILogger<CityController> _logger;
        private readonly IMapper _mapper;
        public CityController(ICityRepository cityRepository, ILogger<CityController> logger, IMapper mapper)
        {
            _cityRepository = cityRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/<CityController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            _logger.LogInformation("Getting list of all City's");
            var result = await _cityRepository.GetCitysAsync();
            return Ok(result);
        }

        // GET api/<CityController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting Id {id} City");
            var result= await _cityRepository.GetCityAsync(id);
            return Ok(result);  
        }

        // POST api/<CityController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CityVm cityVm)
        {
            _logger.LogInformation("add new City");
            var city=_mapper.Map<CityVm,City>(cityVm);
            var result = await _cityRepository.AddCityAsync(city);
            return Ok(result);
        }

        // PUT api/<CityController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CityVm cityVm)
        {
            _logger.LogInformation($"Update Id: {id} City");
            var city = _mapper.Map<CityVm, City>(cityVm);
            var result = await _cityRepository.UpdateCityAsynce(id,city);
            return Ok(result);
        }

        // DELETE api/<CityController>/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            _logger.LogInformation($"Deleted  {id}  City");
            await _cityRepository.DeleteCityAsync(id);
        }
    }
}
