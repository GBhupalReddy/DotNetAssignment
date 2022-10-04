using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using BookMyShow.Core.Dto;
using BookMyShow.Core.Entities;
using BookMyShow.Infrastructure.Specs;
using BookMyShow.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize]
    [Route("showseat")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShowSeatController : ApiControllerBase
    {
        private readonly IShowSeatService _showSeatService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<ShowSeatController> _logger;
        private readonly IMapper _mapper;

        public ShowSeatController(IShowSeatService showSeatService, IExceptionService exceptionService, ILogger<ShowSeatController> logger, IMapper mapper)
        {
            _showSeatService = showSeatService;
            _exceptionService = exceptionService;   
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ShoeSeatController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ShowSeatDto>>> GetShowSeats()
        {
            _logger.LogInformation("Getting list of all ShowSeats");
            var result = await _showSeatService.GetShowSeatsAsync();
            return Ok(result);
        }

        // GET <ShoeSeatController>/5
        [ApiVersion("1.0")]
        [Route("{id}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<ShowSeatDto>> GetShowSeat(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id : {id} ShowSeat", id);
            var showSeaRtesult = await _showSeatService.GetShowSaetByIdAsync(id);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            if (result is null)
                await _exceptionService.VerifyIdExist(id,"ShowSeat");
            return Ok(result);
        }

        // POST <ShoeSeatController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<ShowSeatDto>> PostShowSeat([FromBody] ShowSeatVm showSeatVm)
        {
            _logger.LogInformation("add new ShowSeat");
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var showSeaRtesult = await _showSeatService.AddShowSeatAsync(showSeat);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            return Ok(result);
        }

        // PUT <ShoeSeatController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<ShowSeatDto>> PutShowSeat(int id, [FromBody] ShowSeatVm showSeatVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id} ", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} ShowSeat", id);
            var showSeat = _mapper.Map<ShowSeatVm, ShowSeat>(showSeatVm);
            var showSeaRtesult = await _showSeatService.UpdateShowSeatAsynce(id, showSeat);
            var result = _mapper.Map<ShowSeat, ShowSeatDto>(showSeaRtesult);
            if(result is null)
                await _exceptionService.VerifyIdExist(id,"ShowSeat");
            return Ok(result);
        }

        // DELETE <ShoeSeatController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task DeleteShowSeat(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted Id :  {id}  ShowSeat", id);
            await _showSeatService.DeleteShowSeatAsync(id);
        }
    }
}



