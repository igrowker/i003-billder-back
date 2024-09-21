using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;


namespace Billder.Application.Repository
{
    public class TrabajoRepository : ITrabajoRepository
    {
        private readonly AppDbContext _context;

        public TrabajoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Trabajo> CrearTrabajoRepository(Trabajo trabajo)
        {
            try
            {
                await _context.Trabajos.AddAsync(trabajo);
                await _context?.SaveChangesAsync();
                return trabajo;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al crear el trabajo", ex);
            }

        }
        public async Task<Trabajo> GetTrabajoByIDRepository(int id)
        {
            try
            {
                return await _context.Trabajos.FindAsync(id);
            }
            catch (Exception ex) {
                throw new Exception("Ocurrio un error al obtener el trabajo por ID ", ex);
            }
        }
        public async Task<Trabajo> UpdateTrabajoRepository( Trabajo trabajoRecibido)
        {
            try
            {
                var objetoTrabajo = await _context.Trabajos.FindAsync(trabajoRecibido.Id);

                if (objetoTrabajo == null)
                {
                    return null;
                }
                objetoTrabajo.Nombre = trabajoRecibido.Nombre;
                objetoTrabajo.Descripcion = trabajoRecibido.Descripcion;
                objetoTrabajo.Fecha = trabajoRecibido.Fecha;
                objetoTrabajo.EstadoTrabajo = trabajoRecibido.EstadoTrabajo;
                await _context.SaveChangesAsync();
                return objetoTrabajo;
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception("Ocurrio un error al actualizar el trabajo", ex);
            }

        }
        public async Task<int> DeleteTrabajoRepository(int id)
        {
            var trabajoEncontrado = await _context.Trabajos.FindAsync(id);
            if(trabajoEncontrado == null)
            {
                return 0;
            }
            _context.Trabajos.Remove(trabajoEncontrado);
            return await _context.SaveChangesAsync(); //devuelve el N° de filas afectadas
        }

        public async Task<List<Trabajo>> GetHistorialDeTrabajosRepository(int clienteID, int numeroPagina)
        {
            try
            {
                var clienteValido = await _context.Clientes.FindAsync(clienteID);
                if (clienteValido == null)
                {
                    throw new Exception("Cliente no encontrado");
                }
                int trabajosPorPagina = 5;
                int offset = (numeroPagina - 1) * trabajosPorPagina;

                var trabajosDeCliente = await _context.Trabajos
                    .FromSqlRaw(
                        "SELECT t.Id, t.Nombre, t.ClienteId, t.PresupuestoId, t.Descripcion, " +
                        "t.Fecha, t.EstadoTrabajo, c.Nombre AS ClienteNombre " +
                        "FROM dbo.Trabajo AS t " +
                        "INNER JOIN dbo.Cliente AS c ON t.ClienteId = c.Id " +
                        "WHERE t.ClienteId = {0} " +
                        "ORDER BY t.Fecha DESC " +
                        "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                        clienteID, offset, trabajosPorPagina)
                    .ToListAsync();
                return trabajosDeCliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el historial de trabajos", ex);
            }
        }
    }
}
