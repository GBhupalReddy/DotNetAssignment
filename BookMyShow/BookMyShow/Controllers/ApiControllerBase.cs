using Microsoft.AspNetCore.Mvc;

namespace BookMyShow.Controllers
{
    [Route("v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ApiControllerBase : ControllerBase
    {
    }
}
