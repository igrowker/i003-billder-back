using Billder.Application.Interfaces;
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
                _context?.SaveChangesAsync();
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

                if (objetoTrabajo != null)
                {
                    objetoTrabajo.Nombre = trabajoRecibido.Nombre;
                    objetoTrabajo.Descripcion = trabajoRecibido.Descripcion;
                    objetoTrabajo.Fecha = trabajoRecibido.Fecha;
                    objetoTrabajo.EstadoTrabajo = trabajoRecibido.EstadoTrabajo;
                }

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
            if(trabajoEncontrado != null)
            {
                return 0;
            }
            _context.Trabajos.Remove(trabajoEncontrado);
            return await _context.SaveChangesAsync(); //devuelve el N° de filas afectadas
        }

        //recibo por parametro una lista de ID's, y devuelvo una lista de trabajos
        public async Task<List<Trabajo>> GetHistorialDeTrabajosRepository(int usuarioID)
        {

            var usuarioValido = await _context.Usuarios.FindAsync(usuarioID);
            if (usuarioID != null)
            {
                var trabajosDeUsuario = await _context.Database.ExecuteSqlRawAsync("");
            }
            return null;
            //recibo un usuarioID
            //busco en DB los trabajos que tengan ese usuarioID
            //hago un ordenamiento, traigo los mas recientes primero
            //y establezco un limite de 5, tipo paginacion
        }
    }
}
