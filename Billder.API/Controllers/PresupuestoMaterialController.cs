using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PresupuestoMaterialController : ControllerBase
    {
        private readonly IPresupuestoMaterialService _PresupuestoMaterialService;

        public PresupuestoMaterialController(IPresupuestoMaterialService resupuestoMaterialService)
        {
            _PresupuestoMaterialService = resupuestoMaterialService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PresupuestoMaterial>>> GetAll()
        {
            try
            {
                var presupuestoMaterials= await _PresupuestoMaterialService.GetAllPresupuestoMaterialAsync();
                return Ok(presupuestoMaterials);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<PresupuestoMaterial>> GetById(int id)
        {
            try
            {
                var presupuestoMaterial = await _PresupuestoMaterialService.GetPresupuestoMaterialByIdAsync(id);
                if (presupuestoMaterial == null)
                {
                    return NotFound();
                }
                return Ok(presupuestoMaterial);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }
        [HttpPost]
        public async Task<ActionResult<PresupuestoMaterial>> Create([FromBody] PresupuestoMaterial presupuestoMaterial)
        {
            if (presupuestoMaterial == null)
            {
                return BadRequest();
            }
            try
            {
                var createdPresupuestoMaterial = await _PresupuestoMaterialService.CreatePresupuestoMaterialAsync(presupuestoMaterial);
                return CreatedAtAction(nameof(GetById), new { id = createdPresupuestoMaterial.Id }, createdPresupuestoMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PresupuestoMaterial presupuestoMaterial)
        {

            if (id != presupuestoMaterial.Id)
            {
                return BadRequest();
            }
            try
            {
                var updated = await _PresupuestoMaterialService.UpdatePresupuestoMaterialAsync(presupuestoMaterial);
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
                var deleted = await _PresupuestoMaterialService.DeletePresupuestoMaterialByIdAsync(id);
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
