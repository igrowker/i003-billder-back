using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billder.Application.Repository
{
    public class URegistradoRepository : IURegistradoRepository
    {
        private readonly AppDbContext _context;

        public URegistradoRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<UsuarioRegistrado> CrearUsuarioRegistradoRepository(UsuarioRegistrado usuario)
        {
            try
            {
                await _context.UsuarioRegistrados.AddAsync(usuario);
                await _context.SaveChangesAsync();
                return usuario;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al crear el usuario", ex);
            }
        }

        public async Task<int> DeleteUsuarioRegistradoRepository(int id)
        {
            var usuarioEncontrado = await _context.UsuarioRegistrados.FindAsync(id);
            if (usuarioEncontrado == null)
            {
                return 0;
            }
            _context.UsuarioRegistrados.Remove(usuarioEncontrado);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<UsuarioRegistrado>> GetAllUsuariosRegistradosRepository(int usuarioID, int numeroPagina)
        {
            try
            {
                var usuarioValido = await _context.UsuarioRegistrados.FindAsync(usuarioID);
                if (usuarioValido == null)
                {
                    throw new Exception("El Usuario no pudo ser encontrado");
                }
                int usuariosPorPagina = 15;
                int offset = (numeroPagina - 1) * usuariosPorPagina;

                var allUsers = await _context.UsuarioRegistrados
            .FromSqlRaw(
                "SELECT t.Id, t.Nombre, t.ClienteId, t.PresupuestoId, t.Descripcion, " +
                "t.Fecha, t.EstadoTrabajo, u.FullName AS ClienteNombre " +
                "FROM dbo.Trabajo AS t " +
                "INNER JOIN dbo.Cliente AS c ON t.ClienteId = c.Id " +
                "INNER JOIN dbo.UsuarioRegistrado AS u ON c.UsuarioRegistradoId = u.Id " +
                "WHERE t.ClienteId = {0} " +
                "ORDER BY t.Fecha DESC " +
                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                usuarioID, offset, usuariosPorPagina)
            .ToListAsync();
                return allUsers;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el historial de trabajos", ex);
            }
        }

        public async Task<UsuarioRegistrado> GetUsuarioRegistradoByIDRepository(int id)
        {
            try
            {
                return await _context.UsuarioRegistrados.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el usuario por ID ", ex);
            }
        }

        public async Task<UsuarioRegistrado> UpdateUsuarioRegistradoRepository(UsuarioRegistrado usuario)
        {
            try
            {
                var objetoUsuario = await _context.UsuarioRegistrados.FindAsync(usuario.Id);

                if (objetoUsuario == null)
                {
                    return null;
                }
                objetoUsuario.FullName = usuario.FullName;
                objetoUsuario.Email = usuario.Email;
                objetoUsuario.Identificacion = usuario.Identificacion;
                objetoUsuario.NroIdentificacion = usuario.NroIdentificacion;
                objetoUsuario.FechaNacimiento = usuario.FechaNacimiento;
                objetoUsuario.Direccion = usuario.Direccion;
                objetoUsuario.Ciudad = usuario.Ciudad;
                objetoUsuario.Provincia = usuario.Provincia;
                objetoUsuario.Pais = usuario.Pais;
                objetoUsuario.Telefono = usuario.Telefono;
                objetoUsuario.Password = usuario.Password;
                await _context.SaveChangesAsync();
                return objetoUsuario;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al actualizar el usuario", ex);
            }
        }
    }
}
