using Billder.Application.Repository.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ContratoController : ControllerBase
    {

        private readonly IContratoService _service;
        public ContratoController(ContratoService service)
        {
            _service = service;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContratoByID(int id)
        {
            var contrato = await _service.GetContratoByID(id);
            if (contrato == null)
            {
                return NotFound();
            }
            return Ok(contrato);
        }

        [HttpPost]
        public async Task<IActionResult> CrearContrato([FromBody] ContratoDTO contratoDTO)
        {
            if (contratoDTO == null)
            {
                return BadRequest("Contrato no debe estar vacío");
            }

            var objetoContrato = new Contrato
            {
                UsuarioId = contratoDTO.UsuarioId,
                TrabajoId = contratoDTO.TrabajoId,
                PresupuestoId = contratoDTO.PresupuestoId,
                Condiciones = contratoDTO.Condiciones,
                FechaCreacion = contratoDTO.FechaCreacion,
                FechaFirma = contratoDTO.FechaFirma,
                Estado = contratoDTO.Estado,
                FirmaDigital = contratoDTO.FirmaDigital
            };

            var contratoCreado = await _service.CrearContrato(objetoContrato);
            return CreatedAtAction(nameof(GetContratoByID), new { id = contratoCreado.Id }, contratoCreado);
        }


        [HttpPut]
        public async Task<IActionResult> UpdateContrato(ContratoDTO contratoDTO)
        {
            if (contratoDTO == null)
            {
                return BadRequest("El contrato no puede ser nulo");
            }

            var contratoExistente = await _service.GetContratoByID(contratoDTO.Id);
            if (contratoExistente == null)
            {
                return NotFound($"Contrato con ID {contratoDTO.Id} no encontrado");
            }

            var objetoContrato = new Contrato
            {
                Id = contratoDTO.Id, //no se modifica
                UsuarioId = contratoDTO.UsuarioId,
                TrabajoId = contratoDTO.TrabajoId,
                PresupuestoId = contratoDTO.PresupuestoId,
                Condiciones = contratoDTO.Condiciones,
                FechaCreacion = contratoDTO.FechaCreacion,
                FechaFirma = contratoDTO.FechaFirma,
                Estado = contratoDTO.Estado,
                FirmaDigital = contratoDTO.FirmaDigital
            };

            var contratoActualizado = await _service.UpdateContrato(objetoContrato);
            return Ok(contratoActualizado);
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var contratoEncontrado = await _service.GetContratoByID(id);
            if (contratoEncontrado == null)
            {
                return NotFound("No se encontro un contrato con ese ID");
            }
            await _service.DeleteContrato(id);

            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetHistorialDeContrato(int usuarioID, int numeroPagina)
        {
            if (usuarioID == 0)
            {
                return NotFound("No se pudo encontrar al cliente");
            }
            if(numeroPagina < 1)
            {
                return NotFound("Pagina invalida");
            }
            var contrato = await _service.GetHistorialDeContratos(usuarioID, numeroPagina);
            return Ok(contrato);
        }
    }
}

