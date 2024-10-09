
using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Billder.Infrastructure.DTOs;
using System.Security.Claims;
namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly ITrabajoInterface _service;
        private readonly IClienteService _clienteService;
        private readonly IURegistradoInterface _uRegistradoService;
        private readonly ILogger<TrabajoController> _logger;
        public TrabajoController(ITrabajoInterface service, IClienteService clienteService, IURegistradoInterface uRegistradoService, ILogger<TrabajoController> logger)
        {
            _service = service;
            _clienteService = clienteService;
            _uRegistradoService = uRegistradoService;
            _logger = logger;
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
        public async Task<IActionResult> GetTrabajoByID(int id)
        {
            var userId = ObtenerUsuarioId();
            var trabajo = await _service.GetTrabajoByID(id, userId);
            if (trabajo == null)
            {
                return NotFound();
            }
            return Ok(trabajo);
        }

        [Route("trabajocliente")]
        [HttpPost]
        public async Task<IActionResult> CrearTrabajoCliente([FromBody] TrabajoClienteDTO request)
        {
            var userId = ObtenerUsuarioId();
            //es obligatorio que haya un cliente para un trabajo
            if (request.Trabajo == null)
            {
                return BadRequest("Campo Trabajo no debe estar vacío");
            }
            if (request.Cliente == null)
            {
                return BadRequest("Campo Cliente no debe estar vacío");
            }
            var objetoCliente = new Cliente
            {
                UsuarioId = ObtenerUsuarioId(),
                Identificacion = request.Cliente.Identificacion,
                NroIdentificacion = request.Cliente.NroIdentificacion,
                Nombre = request.Cliente.Nombre,
                Descripcion = request.Cliente.Descripcion,
                Email = request.Cliente.Email,
                Telefono = request.Cliente.Telefono,
                Direccion = request.Cliente.Direccion,
                Ciudad = request.Cliente.Ciudad,
                Provincia = request.Cliente.Provincia,
                Pais = request.Cliente.Pais
            };
            var clienteCreado = await _clienteService.CrearCliente(objetoCliente); //primero cliente, luego trabajo
            if (clienteCreado == null)
            {
                return BadRequest("No se pudo crear el cliente");
            }
            var clienteResponse = new ClienteDTO
            {
                Id = clienteCreado.Id,
                UsuarioId = ObtenerUsuarioId(),
                Identificacion = clienteCreado.Identificacion,
                NroIdentificacion = clienteCreado.NroIdentificacion,
                Nombre = clienteCreado.Nombre,
                Descripcion = clienteCreado.Descripcion,
                Email = clienteCreado.Email,
                Telefono = clienteCreado.Telefono,
                Direccion = clienteCreado.Direccion,
                Ciudad = clienteCreado.Ciudad,
                Provincia = clienteCreado.Provincia,
                Pais = clienteCreado.Pais
            };
            var objetoTrabajo = new Trabajo
            {
                UsuarioId = ObtenerUsuarioId(),
                Nombre = request.Trabajo.Nombre,
                ClienteId = clienteCreado.Id, 
                PresupuestoId = request.Trabajo.PresupuestoId,
                Descripcion = request.Trabajo.Descripcion,
                Imagen = request.Trabajo.Imagen,
                Fecha = request.Trabajo.Fecha,
                EstadoTrabajo = request.Trabajo.EstadoTrabajo
            };

            var trabajoCreado  = await _service.CrearTrabajo(objetoTrabajo);

            var trabajoResponse = new TrabajoDTO
            {
                Id = trabajoCreado.Id,
                UsuarioId = ObtenerUsuarioId(),
                Nombre = trabajoCreado.Nombre,
                ClienteId = trabajoCreado.ClienteId,
                PresupuestoId = trabajoCreado.PresupuestoId,
                Descripcion = trabajoCreado.Descripcion,
                Imagen = trabajoCreado.Imagen,
                Fecha = trabajoCreado.Fecha,
                EstadoTrabajo = trabajoCreado.EstadoTrabajo,
            };
            var response = new TrabajoClienteDTO
            {
                Cliente = clienteResponse,
                Trabajo = trabajoResponse
            };
            return CreatedAtAction(nameof(GetTrabajoByID), new { id = trabajoCreado.Id }, response);
        }
        [HttpPost]
        public async Task<IActionResult> CrearTrabajo([FromBody] TrabajoDTO trabajoDTO)
        {
            //es obligatorio que haya un cliente para un trabajo
            if (trabajoDTO == null)
            {
                return BadRequest("Trabajo no debe estar vacío");
            }
            var objetoTrabajo = new Trabajo
            {
                UsuarioId = ObtenerUsuarioId(),
                Nombre = trabajoDTO.Nombre,
                ClienteId = trabajoDTO.ClienteId,
                PresupuestoId = trabajoDTO.PresupuestoId,
                Descripcion = trabajoDTO.Descripcion,
                Fecha = trabajoDTO.Fecha,
                Imagen = trabajoDTO.Imagen,
                EstadoTrabajo = trabajoDTO.EstadoTrabajo
            };

            var trabajoCreado = await _service.CrearTrabajo(objetoTrabajo);
            return CreatedAtAction(nameof(GetTrabajoByID), new { id = trabajoCreado.Id }, trabajoCreado);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTrabajo(TrabajoDTO trabajoDTO)
        {
            var userId = ObtenerUsuarioId();
            if (trabajoDTO == null)
            {
                return BadRequest("El trabajo no puede ser nulo");
            }
            var trabajoExistente = await _service.GetTrabajoByID(trabajoDTO.Id, userId);
            if (trabajoExistente == null)
            {
                return NotFound($"Trabajo con ID {trabajoDTO.Id} no encontrado");
            }
            var objetoTrabajo = new Trabajo
            {
                Id = trabajoDTO.Id, //esto no se modifica, solo se usa para getByID
                UsuarioId = trabajoDTO.UsuarioId,
                Nombre = trabajoDTO.Nombre,
                ClienteId = trabajoDTO.ClienteId,
                PresupuestoId = trabajoDTO.PresupuestoId,
                Descripcion = trabajoDTO.Descripcion,
                Fecha = trabajoDTO.Fecha,
                Imagen = trabajoDTO.Imagen,
                EstadoTrabajo = trabajoDTO.EstadoTrabajo
            };
            var trabajoEncontrado = await _service.UpdateTrabajo(objetoTrabajo, userId);
            return Ok(trabajoEncontrado);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrabajo(int id)
        {
            var userId = ObtenerUsuarioId();
            var trabajoEncontrado = await _service.GetTrabajoByID(id, userId);
            if (trabajoEncontrado == null)
            {
                return NotFound("No se encontro un trabajo con ese ID");
            }
            var trabajoBorrado = await _service.DeleteTrabajo(id, userId);
            if (trabajoBorrado == 0)
            {
                return NotFound("No se encontro un trabajo para eliminar");
            }
            return NoContent();
        }
        [HttpGet]
        public async Task<IActionResult> GetHistorialDeTrabajos(int usuarioID, int numeroPagina, string ordenamiento)
        {
            if (usuarioID <= 0)
            {
                return NotFound("No se pudo encontrar al usuario");
            }
            if(ordenamiento != "ASC" && ordenamiento != "DESC") //solo acepta uno a la vez, y debe coincidir con esos valores
            {
                return BadRequest("El ordenamiento es invalido");
            }

            var trabajos = await _service.GetHistorialDeTrabajos(usuarioID, numeroPagina, ordenamiento);
            return Ok(trabajos);
        }
    }
}

        

