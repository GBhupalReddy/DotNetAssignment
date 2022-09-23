﻿using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Repository;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [ApiVersion("1.1")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SeatTypePriceController : ApiControllerBase
    {
        private readonly ISeatTypePriceService _seatTypePriceService;
        private readonly ILogger<SeatTypePriceController> _logger;  
        private readonly IMapper _mapper;

        public SeatTypePriceController(ISeatTypePriceService seatTypePriceService, IMapper mapper, ILogger<SeatTypePriceController> logger)
        {
            _seatTypePriceService = seatTypePriceService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: api/<SeatTypePriceController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get()
        {
            var result = await _seatTypePriceService.GetSeatTypePrices();
            return Ok(result);
        }

        // GET api/<SeatTypePriceController>/5
        [ApiVersion("1.0")]
        [Route("{seatType}")]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult> Get(int seatType)
        {
            var seatTypePrice = await _seatTypePriceService.GetSeatTypePriceBYTypeAsync(seatType);
            return Ok(seatTypePrice);
        }

        // POST api/<SeatTypePriceController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<SeatTypePrice> Post([FromBody] SeatTypePriceVm seatTypePriceVm)
        {
            var seatTypePrice = _mapper.Map<SeatTypePriceVm, SeatTypePrice>(seatTypePriceVm);
            var result = await _seatTypePriceService.AddSeatTypePriceAsync(seatTypePrice);
            return result;

        }

        // PUT api/<SeatTypePriceController>/5
        [ApiVersion("1.0")]
        [Route("{seatType}")]
        [HttpPut]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult> Put(int seatType, [FromBody] SeatTypePriceVm seatTypePriceVm)
        {
            var seatTypePrice = _mapper.Map<SeatTypePriceVm, SeatTypePrice>(seatTypePriceVm);
            var result = await _seatTypePriceService.UpdateSeatTypePrice(seatType,seatTypePrice);
            return Ok(result);
        }

        // DELETE api/<SeatTypePriceController>/5
        [HttpDelete("{seatType}")]
        public async Task Delete(int seatType)
        {
            await _seatTypePriceService.DeleteSeatTypePrice(seatType);
        }
    }
}