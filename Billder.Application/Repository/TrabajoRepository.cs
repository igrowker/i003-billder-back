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
            await _context.Trabajos.AddAsync(trabajo);
            await _context.SaveChangesAsync();
            return trabajo;
        }
        public async Task<Trabajo> GetTrabajoByIDRepository(int id)
        {
                return await _context.Trabajos.FindAsync(id);
        }
        public async Task<Trabajo> UpdateTrabajoRepository(Trabajo trabajoRecibido)
        {
            var objetoTrabajo = await _context.Trabajos.FindAsync(trabajoRecibido.Id);

            objetoTrabajo.Nombre = trabajoRecibido.Nombre;
            objetoTrabajo.Descripcion = trabajoRecibido.Descripcion;
            objetoTrabajo.Fecha = trabajoRecibido.Fecha;
            objetoTrabajo.EstadoTrabajo = trabajoRecibido.EstadoTrabajo;
            await _context.SaveChangesAsync();
            return objetoTrabajo;
        }
        
        public async Task<int> DeleteTrabajoRepository(int id)
        {
            var trabajoEncontrado = await _context.Trabajos.FindAsync(id);
            if (trabajoEncontrado == null)
            {
                return 0;
            }
            _context.Trabajos.Remove(trabajoEncontrado);
            return await _context.SaveChangesAsync();
        }

        //agregar otros ordenamientos de ser necesario
        public async Task<List<Trabajo>> GetHistorialDeTrabajosRepository(int usuarioID, int numeroPagina, string ordenamiento)
        {
            var usuarioValido = await _context.UsuarioRegistrados.FindAsync(usuarioID);
            if (usuarioValido == null)
            {
                throw new Exception("Usuario no encontrado");
            }
            int trabajosPorPagina = 5;
            int offset = (numeroPagina - 1) * trabajosPorPagina;
            var trabajosDeUsuario = await _context.Trabajos
                .FromSqlRaw(
                    "SELECT t.Id, t.Nombre, t.ClienteId, t.UsuarioId, t.PresupuestoId, t.Descripcion, " +
                    "t.Fecha, t.EstadoTrabajo, u.FullName AS NombreUsuario " +
                    "FROM dbo.Trabajo AS t " +
                    "INNER JOIN dbo.UsuarioRegistrado AS u ON t.UsuarioId = u.Id " +
                    "WHERE t.UsuarioId = {0} " +
                    "ORDER BY t.Fecha {ordenamiento} " +
                    "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                    usuarioID, offset, trabajosPorPagina)
                .ToListAsync();
            return trabajosDeUsuario;
        }
    }
}
