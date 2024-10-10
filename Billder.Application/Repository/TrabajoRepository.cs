using Billder.Application.Custom;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
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
            await _context.Trabajos.AddAsync(trabajo);
            await _context.SaveChangesAsync();
            return trabajo;
        }
        public async Task<Trabajo> GetTrabajoByIDRepository(int id, int userId)
        {
                return await _context.Trabajos.FirstOrDefaultAsync(t => t.UsuarioId == userId && t.Id == id);
        }
        public async Task<Trabajo> UpdateTrabajoRepository(Trabajo trabajoRecibido, int userId)
        {
            var objetoTrabajo = await _context.Trabajos.FirstOrDefaultAsync(t => t.UsuarioId == userId && t.Id == trabajoRecibido.Id);

            objetoTrabajo.Nombre = trabajoRecibido.Nombre;
            objetoTrabajo.Descripcion = trabajoRecibido.Descripcion;
            objetoTrabajo.Fecha = trabajoRecibido.Fecha;
            objetoTrabajo.EstadoTrabajo = trabajoRecibido.EstadoTrabajo;
            objetoTrabajo.Imagen = trabajoRecibido.Imagen;
            await _context.SaveChangesAsync();
            return objetoTrabajo;
        }
        
        public async Task<int> DeleteTrabajoRepository(int id, int userId)
        {
            var trabajoEncontrado = await _context.Trabajos.FirstOrDefaultAsync(t => t.UsuarioId == userId && t.Id == id);
            if (trabajoEncontrado == null)
            {
                return 0;
            }
            _context.Trabajos.Remove(trabajoEncontrado);
            return await _context.SaveChangesAsync();
        }

        public async Task<Paginacion<TrabajoDTO>> GetHistorialDeTrabajosRepository(int usuarioId, int userId, int numeroPagina, string ordenamiento)
        {
            var usuarioValido = await _context.UsuarioRegistrados.FirstOrDefaultAsync(u => u.Id == userId && u.Id == usuarioId);

            int trabajosPorPagina = 10;
            int offset = (numeroPagina - 1) * trabajosPorPagina;

            string countQuery = "SELECT COUNT(*) FROM dbo.Trabajo WHERE UsuarioId = @userId";
            int cantidadDeTrabajos = await _context.Trabajos
                .Where(t => t.UsuarioId == userId)
                .CountAsync();

            string query =
                "SELECT t.Id, t.Nombre, t.ClienteId, t.UsuarioId, t.PresupuestoId, t.Descripcion, " +
                "t.Fecha, t.EstadoTrabajo " +
                "FROM dbo.Trabajo AS t " +
                "WHERE t.UsuarioId = {0} " +
                $"ORDER BY t.Fecha {ordenamiento} " +
                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY";

            var trabajosDeUsuario = await _context.Trabajos
                .FromSqlRaw(query, userId, offset, trabajosPorPagina)
                .Select(t => new TrabajoDTO
                {
                    Id = t.Id,
                    Nombre = t.Nombre,
                    ClienteId = t.ClienteId,
                    UsuarioId = t.UsuarioId,
                    PresupuestoId = t.PresupuestoId,
                    Descripcion = t.Descripcion,
                    Fecha = t.Fecha,
                    EstadoTrabajo = t.EstadoTrabajo,
                }).ToListAsync();

            return new Paginacion<TrabajoDTO>
            {
                Trabajos = trabajosDeUsuario,
                CantidadTotal = cantidadDeTrabajos,
                PaginaActual = numeroPagina,
                PageSize = trabajosPorPagina,
                TotalDePaginas = (int)Math.Ceiling(cantidadDeTrabajos / (double)trabajosPorPagina)
            };
        }
    }
}
