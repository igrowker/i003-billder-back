
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
            //es obligatorio que haya un cliente para un trabajo
            if (trabajoDTO == null)
            {
                return BadRequest("Trabajo no debe estar vacío");
            }
            var objetoTrabajo = new Trabajo
            {
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
        public async Task<IActionResult> UpdateTrabajo(TrabajoDTO trabajoDTO)
        {
            if (trabajoDTO == null)
            {
                return BadRequest("El trabajo no puede ser nulo");
            }
            var trabajoExistente = await _service.GetTrabajoByID(trabajoDTO.Id);
            if (trabajoExistente == null)
            {
                return NotFound($"Trabajo con ID {trabajoDTO.Id} no encontrado");
            }
            var objetoTrabajo = new Trabajo
            {
                Id = trabajoDTO.Id, //esto no se modifica, solo se usa para getByID
                UsuarioId = trabajoDTO.UsuarioId,
                Nombre = trabajoDTO.Nombre,
                ClienteId = trabajoDTO.ClienteId,
                PresupuestoId = trabajoDTO.PresupuestoId,
                Descripcion = trabajoDTO.Descripcion,
                Fecha = trabajoDTO.Fecha,
                EstadoTrabajo = trabajoDTO.EstadoTrabajo
            };
            var trabajoEncontrado = await _service.UpdateTrabajo(objetoTrabajo);
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
        public async Task<IActionResult> GetHistorialDeTrabajos(int usuarioID, int numeroPagina, string ordenamiento)
        {
            if (usuarioID <= 0)
            {
                return NotFound("No se pudo encontrar al usuario");
            }
            if(ordenamiento != "ASC" && ordenamiento != "DESC") //solo acepta uno a la vez, y debe coincidir con esos valores
            {
                return BadRequest("El ordenamiento es invalido");
            }

            var trabajos = await _service.GetHistorialDeTrabajos(usuarioID, numeroPagina, ordenamiento);
            return Ok(trabajos);
        }
    }
}

        

