﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class CinemaSeatController : ApiControllerBase
    {

        private readonly ICinemaSeatService _cinemaSeatService;
        private readonly ILogger<CinemaSeatController> _logger;
        private readonly IMapper _mapper;
        public CinemaSeatController(ICinemaSeatService cinemaSeatService, ILogger<CinemaSeatController> logger, IMapper mapper)
        {

            _cinemaSeatService = cinemaSeatService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ValuesController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<CinemaSeatDto>>> Get()
        {
            _logger.LogInformation("Getting list of all CinemaSeats");
            var result = await _cinemaSeatService.GetCinemaSeatsAsync();
            return Ok(result);
        }

        // GET <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Getting Id {id} CinemaSeat", id);
            var cinemaSeat = await _cinemaSeatService.GetCinemaSeatByIdAsync(id);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <ValuesController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] CinemaSeatVm cinemaSeatVm)
        {
            _logger.LogInformation("add new CinemaSeat");
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatService.AddCinemaSeatAsync(cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            return Ok(result);
        }

        // PUT <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] CinemaSeatVm cinemaSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} CinemaSeat", id);
            var cinemaSeat = _mapper.Map<CinemaSeatVm, CinemaSeat>(cinemaSeatVm);
            var cinemaSeatResult = await _cinemaSeatService.UpdateCinemaSeatAsynce(id, cinemaSeat);
            var result = _mapper.Map<CinemaSeat, CinemaSeatDto>(cinemaSeat);
            return Ok(result);
        }

        // DELETE <ValuesController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Deleted  {id}  CinemaSeat", id);
            await _cinemaSeatService.DeleteCinemaSeatAsync(id);
        }
    }
}