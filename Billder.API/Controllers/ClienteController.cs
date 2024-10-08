using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClienteController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDTO>> GetClienteById(int id)
        {
            var cliente = await _clienteService.GetClienteByID(id);

            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDTO>> CrearCliente([FromBody] ClienteDTO clienteDTO)
        {
            if (clienteDTO == null)
            {
                return BadRequest("Cliente no debe estar vacío");
            }

            var objetoCliente = new Cliente
            {
                UsuarioId = clienteDTO.UsuarioId,
                Identificacion = clienteDTO.Identificacion,
                NroIdentificacion = clienteDTO.NroIdentificacion,
                Nombre = clienteDTO.Nombre,
                Descripcion = clienteDTO.Descripcion,
                Email = clienteDTO.Email,
                Telefono = clienteDTO.Telefono,
                Direccion = clienteDTO.Direccion,
                Ciudad = clienteDTO.Ciudad,
                Provincia = clienteDTO.Provincia,
                Pais = clienteDTO.Pais,
                FechaAlta = clienteDTO.FechaAlta
            };

            var clienteCreado = await _clienteService.CrearCliente(objetoCliente);

            return CreatedAtAction(nameof(GetClienteById), new { id = clienteCreado.Id }, clienteCreado);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCliente([FromBody] ClienteDTO cliente)
        {


            var clienteEncontrado = await _clienteService.GetClienteByID(cliente.Id);
            if (clienteEncontrado == null)
            {
                return NotFound();
            }

            var updated = await _clienteService.UpdateCliente(cliente);
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var clienteEncontrado = await _clienteService.GetClienteByID(id);
            if (clienteEncontrado == null)
            {
                return NotFound();
            }
            var deleted = await _clienteService.DeleteCliente(id);

            return NoContent();
        }
    }
}
