using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Entities;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("seattypeprice")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class SeatTypePriceController : ApiControllerBase
    {
        private readonly ISeatTypePriceService _seatTypePriceService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<SeatTypePriceController> _logger;
        private readonly IMapper _mapper;

        public SeatTypePriceController(ISeatTypePriceService seatTypePriceService, IExceptionService exceptionService, IMapper mapper, ILogger<SeatTypePriceController> logger)
        {
            _seatTypePriceService = seatTypePriceService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }


        // GET: api/<SeatTypePriceController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<SeatTypePrice>>> Get()
        {
            var result = await _seatTypePriceService.GetSeatTypePrices();
            return Ok(result);
        }

        // GET api/<SeatTypePriceController>/5
        [ApiVersion("1.0")]
        [Route("{seatType}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<SeatTypePrice>> Get(int seatType)
        {
            if (seatType <= 0)
            {
                _logger.LogWarning($"Id field can't be <= zero OR it doesn't match with model's {seatType}");
                await _exceptionService.VerifyIdExist(seatType);
            }
            var seatTypePrice = await _seatTypePriceService.GetSeatTypePriceBYTypeAsync(seatType);
            if (seatTypePrice is null)
            {
                await _exceptionService.VerifyIdExist(seatType);
            }
            return Ok(seatTypePrice);
        }

        // POST api/<SeatTypePriceController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<SeatTypePrice>> Post([FromBody] SeatTypePriceVm seatTypePriceVm)
        {
            var seatTypePrice = _mapper.Map<SeatTypePriceVm, SeatTypePrice>(seatTypePriceVm);
            var result = await _seatTypePriceService.AddSeatTypePriceAsync(seatTypePrice);
            return Ok(result);

        }

        // PUT api/<SeatTypePriceController>/5
        [ApiVersion("1.0")]
        [Route("{seatType}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<SeatTypePrice>> Put(int seatType, [FromBody] SeatTypePriceVm seatTypePriceVm)
        {
            if (seatType <= 0)
            {
                _logger.LogWarning($"Id field can't be <= zero OR it doesn't match with model's {seatType}");
                await _exceptionService.VerifyIdExist(seatType);
            }
            var seatTypePrice = _mapper.Map<SeatTypePriceVm, SeatTypePrice>(seatTypePriceVm);
            var result = await _seatTypePriceService.UpdateSeatTypePrice(seatType, seatTypePrice);
            return Ok(result);
        }

        // DELETE api/<SeatTypePriceController>/5
        [Route("{seatType}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        public async Task Delete(int seatType)
        {
            if (seatType <= 0)
            {
                _logger.LogWarning($"Id field can't be <= zero OR it doesn't match with model's {seatType}");
                await _exceptionService.VerifyIdExist(seatType);
            }
            await _seatTypePriceService.DeleteSeatTypePrice(seatType);
        }
    }
}
