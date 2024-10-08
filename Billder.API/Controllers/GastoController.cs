using Billder.Infrastructure.DTOs;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

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

        [HttpGet("{id}")]
        public async Task<ActionResult<GastoDTO>> GetGastoById(int id)
        {
            var gasto = await _gastoService.GetGastoByID(id);

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
                UsuarioId = gastoDTO.UsuarioId,
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
            if (gastoDTO == null)
            {
                return BadRequest("Gasto no debe estar vacío");
            }

            var gastoEncontrado = await _gastoService.GetGastoByID(gastoDTO.Id);
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

            var updated = await _gastoService.UpdateGasto(objetoGasto);
            return Ok(updated);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGasto(int id)
        {
            var gastoEncontrado = await _gastoService.GetGastoByID(id);
            if (gastoEncontrado == null)
            {
                return NotFound();
            }

            await _gastoService.DeleteGasto(id);
            return NoContent();
        }
    }
}
