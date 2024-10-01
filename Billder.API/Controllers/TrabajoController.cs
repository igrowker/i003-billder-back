
using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Billder.Infrastructure.DTOs;
namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
            if (trabajo == null)
            {
                return NotFound();
            }
            return Ok(trabajo);
        }

        [HttpPost]
        public async Task<IActionResult> CrearTrabajo([FromBody] TrabajoDTO trabajoDTO)
        {
            if (trabajoDTO == null)
            {
                return BadRequest("Trabajo no debe estar vacío");
            }
            var objetoTrabajo = new Trabajo
            {
                Id = trabajoDTO.Id,
                UsuarioId = trabajoDTO.UsuarioId,
                Nombre = trabajoDTO.Nombre,
                ClienteId = trabajoDTO.ClienteId,
                PresupuestoId = trabajoDTO.PresupuestoId,
                Descripcion = trabajoDTO.Descripcion,
                Fecha = trabajoDTO.Fecha,
                EstadoTrabajo = trabajoDTO.EstadoTrabajo
            };

            var trabajoCreado  = await _service.CrearTrabajo(objetoTrabajo);
            return CreatedAtAction(nameof(GetTrabajoByID), new { id = trabajoCreado.Id }, trabajoCreado);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrabajo(Trabajo trabajo)
        {
            if (trabajo == null)
            {
                return BadRequest("El trabajo no puede ser nulo");
            }

            var trabajoEncontrado = await _service.UpdateTrabajo(trabajo);
            return Ok(trabajoEncontrado);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrabajo(int id)
        {
            var trabajoEncontrado = await _service.GetTrabajoByID(id);
            if (trabajoEncontrado == null)
            {
                return NotFound("No se encontro un trabajo con ese ID");
            }
            var trabajoBorrado = await _service.DeleteTrabajo(id);
            if (trabajoBorrado == 0)
            {
                return NotFound("No se encontro un trabajo para eliminar");
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetHistorialDeTrabajos(int clienteID, int numeroPagina)
        {
            if (clienteID == 0)
            {
                return NotFound("No se pudo encontrar al cliente");
            }

            var trabajos = await _service.GetHistorialDeTrabajos(clienteID, numeroPagina);
            return Ok(trabajos);
        }
    }
}

        

