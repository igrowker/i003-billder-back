using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<Contrato> GetContratoByIDRepository(int id)
        {
            try
            {
                return await _context.Contratos.FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrio un error al obtener el contrato por ID ", ex);
            }
        }
        public async Task<Contrato> UpdateContratoRepository(Contrato contratoRecibido)
        {
            try
            {
                var objetoContrato = await _context.Contratos.FindAsync(contratoRecibido.Id);

                if (contratoRecibido == null)
                {
                    return null;
                }
                contratoRecibido.Condiciones = contratoRecibido.Condiciones;

                await _context.SaveChangesAsync();
                return contratoRecibido;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al actualizar el contrato", ex);
            }

        }
        public async Task<int> DeleteContratoRepository(int id)
        {
            var contratoEncontrado = await _context.Contratos.FindAsync(id);
            if (contratoEncontrado == null)
            {
                return 0;
            }
            _context.Contratos.Remove(contratoEncontrado);
            return await _context.SaveChangesAsync(); //devuelve el N° de filas afectadas
        }

        //agregar otros ordenamientos de ser necesario
        public async Task<List<Contrato>> GetHistorialDeContratosRepository(int clienteID, int numeroPagina)
        {
            try
            {
                var clienteValido = await _context.Clientes.FindAsync(clienteID);
                if (clienteValido == null)
                {
                    throw new Exception("Cliente no encontrado");
                }
                int contratosPorPagina = 5;
                int offset = (numeroPagina - 1) * contratosPorPagina;

                var contratosDeCliente = await _context.Contratos

                    //modificar query
            .FromSqlRaw(
                "SELECT t.Id, t.Nombre, t.ClienteId, t.PresupuestoId, t.Descripcion, " +
                "t.Fecha, t.EstadoTrabajo, c.Nombre AS ClienteNombre " +
                "FROM dbo.Trabajo AS t " +
                "INNER JOIN dbo.Cliente AS c ON t.ClienteId = c.Id " +
                "WHERE t.ClienteId = {0} " +
                "ORDER BY t.Fecha DESC " +
                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY",
                clienteID, offset, contratosPorPagina)
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
