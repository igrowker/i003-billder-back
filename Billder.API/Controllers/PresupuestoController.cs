using Billder.Application.Interfaces;
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
            try
            {
                var presupuestos = await _presupuestoService.GetAllPresupuestosAsync(userId);
                return Ok(presupuestos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var presupuesto = await _presupuestoService.GetPresupuestoByIdAsync(id, userId);

                if (presupuesto == null)
                {
                    return NotFound();
                }

                return Ok(presupuesto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult<Presupuesto>> Create([FromBody] PresupuestoDTO presupuestoDto)
        {
            if (presupuestoDto == null)
            {
                return BadRequest();
            }
            var createdPresupuesto = new Presupuesto
            {
                EstadoPresupuesto = presupuestoDto.EstadoPresupuesto,
                UsuarioId = ObtenerUsuarioId(),
            };
            try
            {
                await _presupuestoService.CreatePresupuestoAsync(createdPresupuesto);
                return CreatedAtAction(nameof(GetById), new { id = createdPresupuesto.Id }, createdPresupuesto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PresupuestoDTO presupuestoDto)
        {
            if (id != presupuestoDto.Id)
            {
                return BadRequest();
            }
            var userId = ObtenerUsuarioId();
            var presupuesto = new Presupuesto
            {
                Id = presupuestoDto.Id,
                EstadoPresupuesto = presupuestoDto.EstadoPresupuesto
            };
            try
            {
                var updated = await _presupuestoService.UpdatePresupuestoAsync(presupuesto, userId);

                if (!updated)
                {
                    return NotFound();
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var deleted = await _presupuestoService.DeletePresupuestoByIdAsync(id, userId);

                if (!deleted)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
