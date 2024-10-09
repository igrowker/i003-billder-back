using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Billder.Application.Repository
{
    public class ContratoRepository : IContratoRepository
    {
        private readonly AppDbContext _context;

        public ContratoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Contrato> CrearContratoRepository(Contrato contrato)
        {
            try
            {
                await _context.Contratos.AddAsync(contrato);
                await _context.SaveChangesAsync();
                return contrato;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al crear el contrato", ex);
            }

        }
        public async Task<Contrato> GetContratoByIDRepository(int id, int userId)
        {
            try
            {
                return await _context.Contratos.FirstOrDefaultAsync(c => c.UsuarioId == userId && c.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el contrato por ID ", ex);
            }
        }
        public async Task<Contrato> UpdateContratoRepository(Contrato contratoRecibido, int userId)
        {
            try
            {
                var objetoContrato = await _context.Contratos.FirstOrDefaultAsync(c => c.UsuarioId == userId && c.Id == contratoRecibido.Id);

                objetoContrato.Condiciones = contratoRecibido.Condiciones;
                objetoContrato.TrabajoId = contratoRecibido.TrabajoId;
                objetoContrato.PresupuestoId = contratoRecibido.PresupuestoId;
                objetoContrato.FechaFirma = contratoRecibido.FechaFirma;
                objetoContrato.Estado = contratoRecibido.Estado;
                objetoContrato.FirmaDigital = contratoRecibido.FirmaDigital;

                await _context.SaveChangesAsync();

                return objetoContrato;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al actualizar el contrato", ex);
            }
        }

        public async Task<int> DeleteContratoRepository(int id, int userId)
        {
            var contratoEncontrado = await _context.Contratos.FirstOrDefaultAsync(c => c.UsuarioId == userId && c.Id == id);

            _context.Contratos.Remove(contratoEncontrado);
            return await _context.SaveChangesAsync();
        }

        //agregar otros ordenamientos de ser necesario
        public async Task<List<Contrato>> GetHistorialDeContratosRepository(int usuarioID, int numeroPagina)
        {
            try
            {
                int contratosPorPagina = 5;
                int offset = (numeroPagina - 1) * contratosPorPagina;

                var contratosDeCliente = await _context.Contratos
                    .FromSqlRaw(
                        "SELECT * " +
                        "FROM dbo.Contrato AS co " +
                        "WHERE co.UsuarioId = {0} " +
                        "ORDER BY co.FechaCreacion DESC " +
                        "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                        usuarioID, offset, contratosPorPagina)
                    .ToListAsync();

                return contratosDeCliente;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el historial de contratos", ex);
            }
        }
    }
}
