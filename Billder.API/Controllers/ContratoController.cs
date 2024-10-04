using Billder.Application.Interfaces;
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
                Condiciones = contratoDTO.Condiciones
            };

            var contratoCreado = await _service.CrearContrato(objetoContrato);
            return CreatedAtAction(nameof(GetContratoByID), new { id = contratoCreado.Id }, contratoCreado);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateContrato(Contrato contrato)
        {
            if (contrato == null)
            {
                return BadRequest("El contrato no puede ser nulo");
            }

            var trabajoEncontrado = await _service.UpdateContrato(contrato);
            return Ok(trabajoEncontrado);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteContrato(int id)
        {
            var trabajoEncontrado = await _service.GetContratoByID(id);
            if (trabajoEncontrado == null)
            {
                return NotFound("No se encontro un contrato con ese ID");
            }
            var contratoBorrado = await _service.DeleteContrato(id);
            if (contratoBorrado == 0)
            {
                return NotFound("No se encontro un contrato para eliminar");
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetHistorialDeContrato(int clienteID, int numeroPagina)
        {
            if (clienteID == 0)
            {
                return NotFound("No se pudo encontrar al cliente");
            }

            var contrato = await _service.GetHistorialDeContratos(clienteID, numeroPagina);
            return Ok(contrato);
        }
    }
}

