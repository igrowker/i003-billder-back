using Billder.Infrastructure.DTOs;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class GastoController : ControllerBase
    {
        private readonly IGastoService _gastoService;

        public GastoController(IGastoService gastoService)
        {
            _gastoService = gastoService;
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

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoDTO>> GetGastoById(int id)
        {
            var userId = ObtenerUsuarioId();
            var gasto = await _gastoService.GetGastoByID(id, userId);

            if (gasto == null)
            {
                return NotFound();
            }

            return Ok(gasto);
        }

        [HttpPost]
        public async Task<ActionResult<GastoDTO>> CrearGasto([FromBody] GastoDTO gastoDTO)
        {           
            if (gastoDTO == null)
            {
                return BadRequest("Gasto no debe estar vacío");
            }

            var objetoGasto = new Gasto
            {
                UsuarioId = ObtenerUsuarioId(),
                PresupuestoId = gastoDTO.PresupuestoId,
                Nombre = gastoDTO.Nombre,
                Cantidad = gastoDTO.Cantidad,
                Precio = gastoDTO.Precio,
                CostoHoraLaboral = gastoDTO.CostoHoraLaboral,
                HorasTrabajadas = gastoDTO.HorasTrabajadas
            };

            var gastoCreado = await _gastoService.CrearGasto(objetoGasto);

            return CreatedAtAction(nameof(GetGastoById), new { id = gastoCreado.Id }, gastoCreado);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGasto([FromBody] GastoDTO gastoDTO)
        {
            var userId = ObtenerUsuarioId();
            if (gastoDTO == null)
            {
                return BadRequest("Gasto no debe estar vacío");
            }

            var gastoEncontrado = await _gastoService.GetGastoByID(gastoDTO.Id, userId);
            if (gastoEncontrado == null)
            {
                return NotFound($"Gasto con ID {gastoDTO.Id} no encontrado");
            }

            var objetoGasto = new Gasto
            {
                Id = gastoDTO.Id,
                UsuarioId = gastoDTO.UsuarioId,
                PresupuestoId = gastoDTO.PresupuestoId,
                Nombre = gastoDTO.Nombre,
                Cantidad = gastoDTO.Cantidad,
                Precio = gastoDTO.Precio,
                CostoHoraLaboral = gastoDTO.CostoHoraLaboral,
                HorasTrabajadas = gastoDTO.HorasTrabajadas
            };

            var updated = await _gastoService.UpdateGasto(objetoGasto, userId);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var userId = ObtenerUsuarioId();
            var gastoEncontrado = await _gastoService.GetGastoByID(id, userId);
            if (gastoEncontrado == null)
            {
                return NotFound();
            }

            await _gastoService.DeleteGasto(id, userId);
            return NoContent();
        }
    }
}
