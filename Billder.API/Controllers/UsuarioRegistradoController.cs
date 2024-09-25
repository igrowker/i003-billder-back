using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRegistradoController : ControllerBase
    {
        private readonly IURegistradoInterface _service;
        private readonly ILogger<UsuarioRegistradoController> _logger;

        public UsuarioRegistradoController(IURegistradoInterface uRegistradoData, ILogger<UsuarioRegistradoController> logger)
        {
            _service = uRegistradoData;
            _logger = logger;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUsuarioByID(int id)
        {
            var usuario = await _service.GetUsuarioRegistradoByID(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioRegistrado usuario)
        {
            if (usuario == null)
            {
                return BadRequest("El campo Usuario no debe estar vacío");
            }
            try
            {
                var usuarioCreado = await _service.CrearUsuarioRegistrado(usuario);
                return CreatedAtAction(nameof(GetUsuarioByID), new { id = usuarioCreado.Id }, usuarioCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUsuario(UsuarioRegistrado usuario)
        {
            if (usuario == null)
            {
                return BadRequest("Faltan datos al actualizar el usuario");
            }
            try
            {
                var usuarioEncontrado = await _service.UpdateUsuarioRegistrado(usuario);
                return Ok(usuarioEncontrado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al actualizar el trabajo");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var usuarioEncontrado = await _service.GetUsuarioRegistradoByID(id);
            if (usuarioEncontrado == null)
            {
                return NotFound("No se encontro un Usuario con ese ID");
            }
            try
            {
                var usuarioBorrado = await _service.DeleteUsuarioRegistrado(id);
                if (usuarioBorrado == 0)
                {
                    return NotFound("No se encontro un trabajo para eliminar");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el usuario");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios(int clienteID, int numeroPagina)
        {
            if (clienteID == 0)
            {
                return NotFound("No se pudo encontrar al cliente");
            }
            try
            {
                var trabajos = await _service.GetAllUsuariosRegistrados(clienteID,numeroPagina);
                return Ok(trabajos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener el historial");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
