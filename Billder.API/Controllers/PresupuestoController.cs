using Billder.Application.Interfaces;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;


namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PresupuestoController : ControllerBase
    {
        private readonly IPresupuestoService _presupuestoService;

        public PresupuestoController(IPresupuestoService presupuestoService)
        {
            _presupuestoService = presupuestoService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Presupuesto>>> GetPresupuestos()
        {
            var presupuestos = await _presupuestoService.GetAllPresupuestosAsync();
            return Ok(presupuestos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Presupuesto>> GetById(int id)
        {
            var presupuesto = await _presupuestoService.GetPresupuestoByIdAsync(id);

            if (presupuesto == null)
            {
                return NotFound();
            }

            return Ok(presupuesto);
        }

        [HttpPost]
        public async Task<ActionResult<Presupuesto>> Create([FromBody] Presupuesto presupuesto)
        {
            var createdPresupuesto = await _presupuestoService.CreatePresupuestoAsync(presupuesto);
            return CreatedAtAction(nameof(GetById), new { id = createdPresupuesto.Id }, createdPresupuesto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Presupuesto presupuesto)
        {
            if (id != presupuesto.Id)
            {
                return BadRequest();
            }

            var updated = await _presupuestoService.UpdatePresupuestoAsync(presupuesto);

            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _presupuestoService.DeletePresupuestoByIdAsync(id);

            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
