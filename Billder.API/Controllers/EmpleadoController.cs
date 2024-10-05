using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly IEmpleadoService _empleadoService;
        public EmpleadoController(IEmpleadoService empleadoService)
        {
            _empleadoService = empleadoService;
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
        public async Task<ActionResult<IEnumerable<Empleado>>> GetAll()
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var empleados = await _empleadoService.GetAllEmpleadoAsync(userId);
                return Ok(empleados);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetById(int id)
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var empleado = await _empleadoService.GetEmpleadoByIdAsync(id, userId);
                if (empleado == null)
                {
                    return NotFound();
                }
                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Empleado>> Create([FromBody] EmpleadoDTO empleadoDto)
        {
            if (empleadoDto == null)
            {
                return BadRequest();
            }
            var createdEmpleado = new Empleado
            {
                Fullname = empleadoDto.Fullname,
                Identificacion = empleadoDto.Identificacion,
                NroIdentificacion = empleadoDto.NroIdentificacion,
                Puesto = empleadoDto.Puesto,
                UsuarioId = ObtenerUsuarioId()
            };
            try
            {
                await _empleadoService.CreateEmpleadoAsync(createdEmpleado);
                return CreatedAtAction(nameof(GetById), new { id = createdEmpleado.Id }, createdEmpleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmpleadoDTO empleadoDto)
        {
            if (id != empleadoDto.Id)
            {
                return BadRequest();
            }
            var userId =ObtenerUsuarioId();
            var empleado = new Empleado
            {
                Id = empleadoDto.Id,
                Fullname = empleadoDto.Fullname,
                Identificacion = empleadoDto.Identificacion,
                NroIdentificacion = empleadoDto.NroIdentificacion,
                Puesto = empleadoDto.Puesto,
                UsuarioId = userId,
            };
            try
            {
                var updated = await _empleadoService.UpdateEmpleadoAsync(empleado, userId);
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
                var deleted = await _empleadoService.DeleteEmpleadoByIdAsync(id, userId);
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
