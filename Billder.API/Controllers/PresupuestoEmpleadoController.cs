using Billder.Application.Interfaces;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PresupuestoEmpleadoController : ControllerBase
    {
        private readonly IPresupuestoEmpleadoService _PresupuestoEmpleadoService;

        public PresupuestoEmpleadoController(IPresupuestoEmpleadoService resupuestoEmpleadoService)
        {
            _PresupuestoEmpleadoService = resupuestoEmpleadoService;
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
        public async Task<ActionResult<IEnumerable<PresupuestoEmpleado>>> GetAll()
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var presupuestoEmpleados = await _PresupuestoEmpleadoService.GetAllPresupuestoEmpleadoAsync(userId);
                return Ok(presupuestoEmpleados);
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
            
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PresupuestoEmpleado>> GetById(int id)
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var presupuestoEmpleado = await _PresupuestoEmpleadoService.GetPresupuestoEmpleadoByIdAsync(id, userId);
                if (presupuestoEmpleado == null)
                {
                    return NotFound();
                }
                return Ok(presupuestoEmpleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
           
        }
        [HttpPost]
        public async Task<ActionResult<PresupuestoEmpleado>> Create([FromBody]PresupuestoEmpleadoDTO presupuestoEmpleadoDto)
        {
            if(presupuestoEmpleadoDto == null)
            {
                return BadRequest();
            }
            var createdPresupuestoEmpleado = new PresupuestoEmpleado
            {
                PresupuestoId = presupuestoEmpleadoDto.PresupuestoId,
                EmpleadoId = presupuestoEmpleadoDto.EmpleadoId,
                HorasTrabajadas = presupuestoEmpleadoDto.HorasTrabajadas,
                CostoHora = presupuestoEmpleadoDto.CostoHora,
                UsuarioId = ObtenerUsuarioId()
            };
            try
            {
                await _PresupuestoEmpleadoService.CreatePresupuestoEmpleadoAsync(createdPresupuestoEmpleado);
                return CreatedAtAction(nameof(GetById), new { id = createdPresupuestoEmpleado.Id }, createdPresupuestoEmpleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]PresupuestoEmpleadoDTO presupuestoEmpleadoDto)
        {

            if (id != presupuestoEmpleadoDto.Id)
            {
                return BadRequest();
            }
            var userId = ObtenerUsuarioId();
            var presupuestoEmpleado = new PresupuestoEmpleado
            {
                Id = presupuestoEmpleadoDto.Id,
                PresupuestoId = presupuestoEmpleadoDto.PresupuestoId,
                EmpleadoId = presupuestoEmpleadoDto.EmpleadoId,
                HorasTrabajadas = presupuestoEmpleadoDto.HorasTrabajadas,
                CostoHora = presupuestoEmpleadoDto.CostoHora,
            };
            try
            {
                var updated = await _PresupuestoEmpleadoService.UpdatePresupuestoEmpleadoAsync(presupuestoEmpleado, userId);
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
                var deleted = await _PresupuestoEmpleadoService.DeletePresupuestoEmpleadoByIdAsync(id, userId);
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
