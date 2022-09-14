﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("2.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShowSeatController : ApiControllerBase
    {
        private readonly IShowSeatService _showSeatService;
        private readonly ILogger<ShowSeatController> _logger;
        private readonly IMapper _mapper;

        public ShowSeatController(IShowSeatService showSeatService, ILogger<ShowSeatController> logger, IMapper mapper)
        {
            _showSeatService = showSeatService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ShoeSeatController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ShowSeatDto>>> Get()
        {
            _logger.LogInformation("Getting list of all ShowSeats");
            var result = await _showSeatService.GetShowSeatsAsync();
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // GET <ShoeSeatController>/5
        [ApiVersion("2.0")]
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
            _logger.LogInformation("Getting Id : {id} ShowSeat", id);
            var showSeaRtesult = await _showSeatService.GetShowSaetByIdAsync(id);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            if (result is null)
                return NotFound("Please Enter Valid Data");
            return Ok(result);
        }

        // POST <ShoeSeatController>
        [ApiVersion("2.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> Post([FromBody] ShowSeatVm showSeatVm)
        {
            _logger.LogInformation("add new ShowSeat");
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var showSeaRtesult = await _showSeatService.AddShowSeatAsync(showSeat);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            return Ok(result);
        }

        // PUT <ShoeSeatController>/5
        [ApiVersion("2.0")]
        [Route("{id}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int id, [FromBody] ShowSeatVm showSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogError(new ArgumentOutOfRangeException(nameof(id)), "Id field can't be <= zero OR it doesn't match with model's {Id} ", id);
                return BadRequest("Please Enter Valid Data");
            }
            _logger.LogInformation("Update Id: {id} ShowSeat", id);
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var showSeaRtesult = await _showSeatService.UpdateShowSeatAsynce(id, showSeat);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            return Ok(result);
        }

        // DELETE <ShoeSeatController>/5
        [ApiVersion("2.0")]
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
            _logger.LogInformation("Deleted Id :  {id}  ShowSeat", id);
            await _showSeatService.DeleteShowSeatAsync(id);
        }
    }
}


