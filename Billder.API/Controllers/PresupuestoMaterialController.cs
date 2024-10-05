using Billder.Application.Interfaces;
using Billder.Application.Services;
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
    public class PresupuestoMaterialController : ControllerBase
    {
        private readonly IPresupuestoMaterialService _PresupuestoMaterialService;

        public PresupuestoMaterialController(IPresupuestoMaterialService resupuestoMaterialService)
        {
            _PresupuestoMaterialService = resupuestoMaterialService;
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
        public async Task<ActionResult<IEnumerable<PresupuestoMaterial>>> GetAll()
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var presupuestoMaterials= await _PresupuestoMaterialService.GetAllPresupuestoMaterialAsync(userId);
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
            var userId = ObtenerUsuarioId();
            try
            {
                var presupuestoMaterial = await _PresupuestoMaterialService.GetPresupuestoMaterialByIdAsync(id, userId);
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
        public async Task<ActionResult<PresupuestoMaterial>> Create([FromBody] PresupuestoMaterialDTO presupuestoMaterialDto)
        {
            if (presupuestoMaterialDto == null)
            {
                return BadRequest();
            }
            var createdPresupuestoMaterial = new PresupuestoMaterial
            {
                PresupuestoID = presupuestoMaterialDto.PresupuestoID,
                MaterialID = presupuestoMaterialDto.MaterialID,
                Cantidad = presupuestoMaterialDto.Cantidad,
                Costo = presupuestoMaterialDto.Costo,
                Observacion = presupuestoMaterialDto.Observacion,
                UsuarioId = ObtenerUsuarioId()
            };
            try
            {
                await _PresupuestoMaterialService.CreatePresupuestoMaterialAsync(createdPresupuestoMaterial);
                return CreatedAtAction(nameof(GetById), new { id = createdPresupuestoMaterial.Id }, createdPresupuestoMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] PresupuestoMaterialDTO presupuestoMaterialDto)
        {
            var userId = ObtenerUsuarioId();
            if (id != presupuestoMaterialDto.Id)
            {
                return BadRequest();
            }
            var presupuestoMaterial = new PresupuestoMaterial
            {
                Id = presupuestoMaterialDto.Id,
                PresupuestoID = presupuestoMaterialDto.PresupuestoID,
                MaterialID = presupuestoMaterialDto.MaterialID,
                Cantidad = presupuestoMaterialDto.Cantidad,
                Costo = presupuestoMaterialDto.Costo,
                Observacion = presupuestoMaterialDto.Observacion,
            };
            try
            {
                var updated = await _PresupuestoMaterialService.UpdatePresupuestoMaterialAsync(presupuestoMaterial, userId);
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
                var deleted = await _PresupuestoMaterialService.DeletePresupuestoMaterialByIdAsync(id, userId);
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
