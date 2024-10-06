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
    public class MaterialController : ControllerBase
    {
        private readonly IMaterialService _materialService;

        public MaterialController(IMaterialService materialService)
        {
            _materialService = materialService;
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
        public async Task<ActionResult<IEnumerable<Material>>> GetAll()
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var materiales = await _materialService.GetAllMaterialAsync(userId);
                return Ok(materiales);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Material>> GetById(int id)
        {
            var userId = ObtenerUsuarioId();
            try
            {
                var material = await _materialService.GetMaterialByIdAsync(id, userId);
                if (material == null)
                {
                    return NotFound();
                }
                return Ok(material);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Material>> Create([FromBody] MaterialDTO materialDto)
        {
            if (materialDto == null)
            {
                return BadRequest();
            }
            
            var createdMaterial = new Material
            {
                Descripcion = materialDto.Descripcion,
                UsuarioId = ObtenerUsuarioId(),
            };
            try
            {
                await _materialService.CreateMaterialAsync(createdMaterial);
                return CreatedAtAction(nameof(GetById), new { id = createdMaterial.Id }, createdMaterial);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ocurrió un error inesperado.", detail = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] MaterialDTO materialDto)
        {
            if (id != materialDto.Id)
            {
                return BadRequest();
            }
            var userId = ObtenerUsuarioId();
            var material = new Material
            {
                Id = materialDto.Id,
                Descripcion = materialDto.Descripcion,
            };
            try
            {
                var updated = await _materialService.UpdateMaterialAsync(material, userId);
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
                var deleted = await _materialService.DeleteMaterialByIdAsync(id, userId);
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
