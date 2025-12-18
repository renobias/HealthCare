using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare_ApoointmentAvailability.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = "api-home")]
        public String Get()
        {
            return "welcome to healthcare appointment API";
        }
    }
}
