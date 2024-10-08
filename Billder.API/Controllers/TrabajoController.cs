
using Billder.Application.Interfaces;
using Billder.Application.Services;
using Billder.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Billder.Infrastructure.DTOs;
namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    //[Authorize]
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
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrabajoByID(int id)
        {
            var trabajo = await _service.GetTrabajoByID(id);
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
                UsuarioId = request.Cliente.UsuarioId,
                Identificacion = request.Cliente.Identificacion,
                NroIdentificacion = request.Cliente.NroIdentificacion,
                Nombre = request.Cliente.Nombre,
                Descripcion = request.Cliente.Descripcion,
                Email = request.Cliente.Email,
                Telefono = request.Cliente.Telefono,
                Direccion = request.Cliente.Direccion,
                Ciudad = request.Cliente.Ciudad,
                Provincia = request.Cliente.Provincia,
                Imagen = request.Cliente.Imagen,
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
                UsuarioId = clienteCreado.UsuarioId,
                Identificacion = clienteCreado.Identificacion,
                NroIdentificacion = clienteCreado.NroIdentificacion,
                Nombre = clienteCreado.Nombre,
                Descripcion = clienteCreado.Descripcion,
                Email = clienteCreado.Email,
                Telefono = clienteCreado.Telefono,
                Direccion = clienteCreado.Direccion,
                Ciudad = clienteCreado.Ciudad,
                Provincia = clienteCreado.Provincia,
                Imagen = clienteCreado.Imagen,
                Pais = clienteCreado.Pais
            };
            var objetoTrabajo = new Trabajo
            {
                UsuarioId = request.Trabajo.UsuarioId,
                Nombre = request.Trabajo.Nombre,
                ClienteId = clienteCreado.Id, 
                PresupuestoId = request.Trabajo.PresupuestoId,
                Descripcion = request.Trabajo.Descripcion,
                Fecha = request.Trabajo.Fecha,
                Imagen = request.Trabajo.Imagen,
                EstadoTrabajo = request.Trabajo.EstadoTrabajo
            };

            var trabajoCreado  = await _service.CrearTrabajo(objetoTrabajo);

            var trabajoResponse = new TrabajoDTO
            {
                Id = trabajoCreado.Id,
                UsuarioId = trabajoCreado.UsuarioId,
                Nombre = trabajoCreado.Nombre,
                ClienteId = trabajoCreado.ClienteId,
                PresupuestoId = trabajoCreado.PresupuestoId,
                Descripcion = trabajoCreado.Descripcion,
                Fecha = trabajoCreado.Fecha,
                Imagen = trabajoCreado.Imagen,
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
                UsuarioId = trabajoDTO.UsuarioId,
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
            if (trabajoDTO == null)
            {
                return BadRequest("El trabajo no puede ser nulo");
            }
            var trabajoExistente = await _service.GetTrabajoByID(trabajoDTO.Id);
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
                Imagen = trabajoDTO.Imagen,
                Fecha = trabajoDTO.Fecha,
                EstadoTrabajo = trabajoDTO.EstadoTrabajo
            };
            var trabajoEncontrado = await _service.UpdateTrabajo(objetoTrabajo);
            return Ok(trabajoEncontrado);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteTrabajo(int id)
        {
            var trabajoEncontrado = await _service.GetTrabajoByID(id);
            if (trabajoEncontrado == null)
            {
                return NotFound("No se encontro un trabajo con ese ID");
            }
            var trabajoBorrado = await _service.DeleteTrabajo(id);
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

        

