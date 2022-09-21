using AutoMapper;
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

       
    }
}
