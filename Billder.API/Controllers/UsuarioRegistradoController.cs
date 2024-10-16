﻿
using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Billder.Application.Custom;
using Billder.Infrastructure.DTOs;
using System.Security.Claims;
namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioRegistradoController : ControllerBase
    {
        private readonly IURegistradoInterface _service;
        private readonly ILogger<UsuarioRegistradoController> _logger;
        private readonly Utilidades _utilidades;

        public UsuarioRegistradoController(IURegistradoInterface uRegistradoData, ILogger<UsuarioRegistradoController> logger, Utilidades utilidades)
        {
            _service = uRegistradoData;
            _logger = logger;
            _utilidades = utilidades;
        }
        [Authorize]
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
        public async Task<IActionResult> CrearUsuario([FromBody] UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("El campo Usuario no debe estar vacío");
            }
            var usuarioRegistrado = new UsuarioRegistrado
            {
                FullName = usuarioDTO.FullName,
                Email = usuarioDTO.Email,
                Identificacion = usuarioDTO.Identificacion,
                NroIdentificacion = usuarioDTO.NroIdentificacion,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                Direccion = usuarioDTO.Direccion,
                Ciudad = usuarioDTO.Ciudad,
                Provincia = usuarioDTO.Provincia,
                Pais = usuarioDTO.Pais,
                Telefono = usuarioDTO.Telefono,
                Imagen = usuarioDTO.Imagen,
                Password = _utilidades.encriptarSHA256(usuarioDTO.Password)
            };

            try
            {
                var usuarioCreado = await _service.CrearUsuarioRegistrado(usuarioRegistrado);
                return CreatedAtAction(nameof(GetUsuarioByID), new { id = usuarioCreado.Id }, usuarioCreado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al crear el usuario");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateUsuario(UsuarioDTO usuarioDTO)
        {
            if (usuarioDTO == null)
            {
                return BadRequest("Faltan datos al actualizar el usuario");
            }
            // Map UsuarioDTO to UsuarioRegistrado
            var usuarioToUpdate = new UsuarioRegistrado
            {
                FullName = usuarioDTO.FullName,
                Email = usuarioDTO.Email,
                Identificacion = usuarioDTO.Identificacion,
                NroIdentificacion = usuarioDTO.NroIdentificacion,
                FechaNacimiento = usuarioDTO.FechaNacimiento,
                Direccion = usuarioDTO.Direccion,
                Ciudad = usuarioDTO.Ciudad,
                Provincia = usuarioDTO.Provincia,
                Pais = usuarioDTO.Pais,
                Telefono = usuarioDTO.Telefono,
                Password = _utilidades.encriptarSHA256(usuarioDTO.Password),
                Imagen = usuarioDTO.Imagen
            };
            var usuarioEncontrado = await _service.UpdateUsuarioRegistrado(usuarioToUpdate);
            return Ok(usuarioEncontrado);
        }

        [Authorize]
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
                    return NotFound("No se encontro un usuario para eliminar");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al borrar el usuario");
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetAllUsuarios")]
        public async Task<IActionResult> GetAllUsuarios(int numeroPagina, string ordenamiento)
        {
            try
            {
                if (numeroPagina < 1)
                {
                    throw new Exception("Pagina invalida");
                }
                if (ordenamiento != "ASC" && ordenamiento != "DESC")
                {
                    throw new Exception("Valor de ordenamiento invalido. Debe ser 'ASC' o 'DESC'.");
                }
                var usuarios = await _service.GetAllUsuariosRegistrados(numeroPagina, ordenamiento);
                return Ok(usuarios);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al obtener los usuarios registrados");
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost("CambiarContraseña")]
        [Authorize]
        public async Task<IActionResult> CambiarContraseña([FromBody] ChangePasswordDTO dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
            {
                return Unauthorized("Token inválido o no se ha proporcionado un ID de usuario.");
            }

            var userId = int.Parse(userIdClaim.Value);

            var usuario = await _service.GetUsuarioRegistradoByID(userId);

            if (usuario == null)
            {
                return NotFound("Usuario no encontrado");
            }
            try
            {
                await _service.UpdatePasword(usuario, dto);
                return Ok("Contraseña cambiada exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Ocurrió un error al cambiar la contraseña.");
            }
        }
    }
}