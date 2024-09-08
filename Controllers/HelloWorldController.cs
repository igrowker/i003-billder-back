using Microsoft.AspNetCore.Mvc;

namespace HelloWorldApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HelloWorldController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello World");
        }
    }
}
