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
    [Route("show")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class ShowController : ApiControllerBase
    {
        private readonly IShowService _showService;
        private readonly IExceptionService _exceptionService;
        private readonly ILogger<ShowController> _logger;
        private readonly IMapper _mapper;
        public ShowController(IShowService showService, IExceptionService exceptionService, ILogger<ShowController> logger, IMapper mapper)
        {
            _showService = showService;
            _exceptionService = exceptionService;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: <ShowController>
        [ApiVersion("1.0")]
        [Route(""), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<IEnumerable<ShowDto>>> GetShows()
        {
            _logger.LogInformation("Getting list of all Shows");
            var result = await _showService.GetShowsAsync();
            return Ok(result);
        }

        // GET <ShowController>/5
        [ApiVersion("1.0")]
        [Route("{id}"), AllowAnonymous]
        [HttpGet]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<ShowDto>> GetShow(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Getting Id : {id} Show", id);
            var ShowResult = await _showService.GetShowByIdAsync(id);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            if (result is null)
                await _exceptionService.VerifyIdExist(id, "Show");
            return Ok(result);
        }

        // POST <ShowController>
        [ApiVersion("1.0")]
        [Route("")]
        [HttpPost]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult<ShowDto>> PostShow([FromBody] ShowVm showVm)
        {
            _logger.LogInformation("add new Show");
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var ShowResult = await _showService.AddShowAsync(show);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            return Ok(result);
        }

        // PUT <ShowController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpPut]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put))]
        public async Task<ActionResult<ShowDto>> PutShow(int id, [FromBody] ShowVm showVm)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Update Id: {id} Show", id);
            var show = _mapper.Map<ShowVm, Show>(showVm);
            var ShowResult = await _showService.UpdateShowAsynce(id, show);
            var result = _mapper.Map<Show, ShowDto>(ShowResult);
            if (result is null)
            {
                await _exceptionService.VerifyIdExist(id, "Payment");
            }
            return Ok(result);
        }

        // DELETE <ShowController>/5
        [ApiVersion("1.0")]
        [Route("{id}")]
        [HttpDelete]
        [Authorize(Roles = "admin")]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Delete))]
        public async Task Delete(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("Id field can't be <= zero OR it doesn't match with model's {Id}", id);
                await _exceptionService.VerifyIdExist(id);
            }
            _logger.LogInformation("Deleted Id :  {id}  Show", id);
            await _showService.DeleteShowAsync(id);
        }
    }
}
