
using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly ITrabajoInterface _service;
        private readonly ILogger<TrabajoController> _logger;
        public TrabajoController(TrabajoService service, ILogger<TrabajoController> logger)
        {
            _service = service;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajoByID(int id)
        {
            var trabajo = await _service.GetTrabajoByID(id);
                if(trabajo == null)
            {
                return NotFound();
            }
                return Ok(trabajo);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTrabajo([FromBody] Trabajo trabajo)
        {
            if(trabajo == null)
            {
                return BadRequest("Trabajo no puede ser nulo");
            }
            try
            {
                var trabajoCreado = await _service.CrearTrabajo(trabajo);
                return CreatedAtAction(nameof(GetTrabajoByID), new { id = trabajoCreado.Id }, trabajoCreado);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al crear el trabajo");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
