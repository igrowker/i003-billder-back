
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
                return BadRequest("Trabajo no debe estar vacío");
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
        [HttpPut]
        public async Task<IActionResult> UpdateTrabajo(Trabajo trabajo)
        {
            if(trabajo == null)
            {
                return BadRequest("El trabajo no puede ser nulo");
            }
            try
            {
                var trabajoEncontrado = await _service.UpdateTrabajo(trabajo);
                return Ok(trabajoEncontrado);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el trabajo");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTrabajo(int id)
        {
            var trabajoEncontrado = await _service.GetTrabajoByID(id);
            if(trabajoEncontrado == null)
            {
                return NotFound("No se encontro un trabajo con ese ID");
            }
            try
            {
                var trabajoBorrado = await _service.DeleteTrabajo(id);
                if(trabajoBorrado == 0)
                {
                    return NotFound("No se encontro un trabajo para eliminar");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el trabajo");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetHistorialDeTrabajos(int clienteID, int numeroPagina)
        {
            if(clienteID == 0)
            {
                return NotFound("No se pudo encontrar al cliente");
            }
            try
            {
                var trabajos = await _service.GetHistorialDeTrabajos(clienteID,numeroPagina);
                return Ok(trabajos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el historial");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
