using Billder.Application.Interfaces;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresupuestoEmpleado>>> GetAll()
        {
            try
            {
                var presupuestoEmpleados = await _PresupuestoEmpleadoService.GetAllPresupuestoEmpleadoAsync();
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
            try
            {
                var presupuestoEmpleado = await _PresupuestoEmpleadoService.GetPresupuestoEmpleadoByIdAsync(id);
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
        public async Task<ActionResult<PresupuestoEmpleado>> Create([FromBody]PresupuestoEmpleado presupuestoEmpleado)
        {
            if(presupuestoEmpleado == null)
            {
                return BadRequest();
            }
            try
            {
                var createdPresupuestoEmpleado = await _PresupuestoEmpleadoService.CreatePresupuestoEmpleadoAsync(presupuestoEmpleado);
                return CreatedAtAction(nameof(GetById), new { id = createdPresupuestoEmpleado.Id }, createdPresupuestoEmpleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }
            
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id,[FromBody]PresupuestoEmpleado presupuestoEmpleado)
        {

            if (id != presupuestoEmpleado.Id)
            {
                return BadRequest();
            }
            try
            {
                var updated = await _PresupuestoEmpleadoService.UpdatePresupuestoEmpleadoAsync(presupuestoEmpleado);
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
            try
            {
                var deleted = await _PresupuestoEmpleadoService.DeletePresupuestoEmpleadoByIdAsync(id);
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
