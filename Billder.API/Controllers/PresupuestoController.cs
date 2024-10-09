using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _presupuestoService;

        public PresupuestoController(IPresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }
        private int ObtenerUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new UnauthorizedAccessException("Token inválido o ausente.");
            }

            return int.Parse(userIdClaim.Value);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuestos()
        {
            var userId = ObtenerUsuarioId();
            var presupuestos = await _presupuestoService.GetAllPresupuestosAsync(userId);
            return Ok(presupuestos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var userId = ObtenerUsuarioId();
            var presupuesto = await _presupuestoService.GetPresupuestoByIdAsync(id, userId);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return Ok(presupuesto);
        }

        [HttpPost]
        public async Task<ActionResult<Presupuesto>> Create([FromBody] PresupuestoDTO presupuestoDto)
        {

            if(presupuestoDto == null)
            {
                return BadRequest("Presupuest no debe estar vacío");
            }
            var objetoPresupuesto = new Presupuesto
            {
                UsuarioId = ObtenerUsuarioId(),
                ClienteId = presupuestoDto.ClienteId,
                FechaVencimiento = (DateTime)presupuestoDto.FechaVencimiento,
                EstadoPresupuesto = presupuestoDto.EstadoPresupuesto,
            };
            var createdPresupuesto = await _presupuestoService.CreatePresupuestoAsync(objetoPresupuesto);
            return CreatedAtAction(nameof(GetById), new { id = createdPresupuesto.Id }, createdPresupuesto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PresupuestoDTO presupuestoDto)
        {
            var userId = ObtenerUsuarioId();
            if (presupuestoDto == null)
            {
                return BadRequest("El Presupuesto no puede ser nulo");
            }

            var presupuestoExistente = await _presupuestoService.GetPresupuestoByIdAsync(id, userId);
            if (presupuestoExistente == null)
            {
                return NotFound($"Presupuesto con ID {presupuestoDto.Id} no encontrado");
            }
            var objetoPresupuesto = new Presupuesto
            {
                Id = id,
                ClienteId = presupuestoDto.ClienteId,
                FechaVencimiento = (DateTime)presupuestoDto.FechaVencimiento,
                EstadoPresupuesto = presupuestoDto.EstadoPresupuesto,
            };
            var presupuestoActualizar = await _presupuestoService.UpdatePresupuestoAsync(objetoPresupuesto, userId);
            return Ok(objetoPresupuesto);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = ObtenerUsuarioId();
            var presupuestoExistente = await _presupuestoService.GetPresupuestoByIdAsync(id, userId);
            if (presupuestoExistente == null)
            {
                return NotFound($"Presupuesto con ID {id} no encontrado");
            }
            var deleted = await _presupuestoService.DeletePresupuestoByIdAsync(id, userId);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
