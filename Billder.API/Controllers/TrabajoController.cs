
using Billder.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly ITrabajoInterface _trabajoInterface;
        private readonly ILogger<TrabajoController> _logger;
        public TrabajoController(ITrabajoInterface trabajoInterface, ILogger<TrabajoController> logger)
        {
            _trabajoInterface = trabajoInterface;
            _logger = logger;
        }
    }
}
