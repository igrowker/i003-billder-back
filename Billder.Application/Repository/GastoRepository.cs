using Billder.Infrastructure.Data;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class GastoRepository : IGastoRepository
    {
        private readonly AppDbContext _context;

        public GastoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Gasto> CrearGastoRepository(Gasto gasto)
        {
            await _context.Gastos.AddAsync(gasto);
            await _context.SaveChangesAsync();
            return gasto;
        }

        public async Task<Gasto> GetGastoByIDRepository(int id, int userId)
        {
            return await _context.Gastos.FirstOrDefaultAsync(g => g.UsuarioId == userId && g.Id == id);
        }

        public async Task<Gasto> UpdateGastoRepository(Gasto gastoRecibido, int userId)
        {
            var objetoGasto = await _context.Gastos.FirstOrDefaultAsync(g => g.UsuarioId == userId && g.Id == gastoRecibido.Id);

            if (objetoGasto == null)
            {
                throw new Exception("Gasto no encontrado");
            }

            objetoGasto.Nombre = gastoRecibido.Nombre;
            objetoGasto.Cantidad = gastoRecibido.Cantidad;
            objetoGasto.Precio = gastoRecibido.Precio; 
            objetoGasto.CostoHoraLaboral = gastoRecibido.CostoHoraLaboral;
            objetoGasto.HorasTrabajadas = gastoRecibido.HorasTrabajadas;

            await _context.SaveChangesAsync();
            return objetoGasto;
        }

        public async Task<int> DeleteGastoRepository(int id, int userId)
        {
            var gastoEncontrado = await _context.Gastos.FirstOrDefaultAsync(g => g.UsuarioId == userId && g.Id == id);
            if (gastoEncontrado == null)
            {
                return 0; 
            }
            _context.Gastos.Remove(gastoEncontrado);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<GastoDTO>> GetHistorialDeGastossRepository(int clienteID, int numeroPagina, string ordenamiento)
        {
            var clienteValido = await _context.Clientes.FindAsync(clienteID);
            if (clienteValido == null)
            {
                throw new Exception("Cliente no encontrado");
            }

            int gastosPorPagina = 5;
            int offset = (numeroPagina - 1) * gastosPorPagina;

            string query =
                "SELECT g.Id, g.UsuarioId, g.Nombre, g.Cantidad, g.Precio, g.CostoHoraLaboral, g.HorasTrabajadas " +
                "FROM dbo.Gasto AS g " +
                "WHERE g.ClienteId = {0} " + 
                $"ORDER BY g.Fecha {ordenamiento} " +
                "OFFSET {1} ROWS FETCH NEXT {2} ROWS ONLY";

            var gastosDeCliente = await _context.Gastos
                .FromSqlRaw(query, clienteID, offset, gastosPorPagina)
                .Select(g => new GastoDTO
                {
                    Id = g.Id,
                    UsuarioId = g.UsuarioId, 
                    Nombre = g.Nombre,
                    Cantidad = g.Cantidad,
                    Precio = g.Precio,
                    CostoHoraLaboral = g.CostoHoraLaboral,
                    HorasTrabajadas = g.HorasTrabajadas,
                }).ToListAsync();

            return gastosDeCliente;
        }
    }

}
