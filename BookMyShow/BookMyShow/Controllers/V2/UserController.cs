using AutoMapper;
using BookMyShow.Core.Contracts.Infrastructure.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BookMyShow.Controllers.V2
{
    [ApiVersion("1.0")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UserController : ApiControllerBase
    {

        private readonly IUserService _UserService;
        private readonly ILogger<UserController> _logger;
        private readonly IMapper _mapper;
        public UserController(IUserService userService, ILogger<UserController> logger, IMapper mapper)
        {
            _UserService = userService;
            _logger = logger;
            _mapper = mapper;
        }


      

     

            
     }
}
