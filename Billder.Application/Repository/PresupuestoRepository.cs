using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly AppDbContext _context;

        public PresupuestoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Presupuesto> CreatePresupuesto(Presupuesto presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }

        public async Task<bool> DeletePresupuestoById(int id, int userId)
        {
            var presupuesto = await _context.Presupuestos.Include(p => p.PresupuestoEmpleados)
                                                         .Include(p => p.PresupuestoMaterials)
                                                         .FirstOrDefaultAsync(p => p.Id == id && p.UsuarioId == userId);    

            if (presupuesto == null)
            {
                return false;
            }

            if (presupuesto.PresupuestoEmpleados != null && presupuesto.PresupuestoEmpleados.Any())
            {
                _context.PresupuestoEmpleados.RemoveRange(presupuesto.PresupuestoEmpleados);
            }
            if (presupuesto.PresupuestoMaterials != null && presupuesto.PresupuestoMaterials.Any())
            {
                _context.PresupuestoMaterials.RemoveRange(presupuesto.PresupuestoMaterials);
            }
            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Presupuesto>> GetAllPresupuesto(int userId)
        {
            return await _context.Presupuestos.Where(p => p.UsuarioId == userId).ToListAsync();
        }

        public async Task<Presupuesto> GetPresupuestoById(int id, int userId)
        {
            var result = await _context.Presupuestos.FirstOrDefaultAsync(p => p.UsuarioId == userId && p.Id == id); 
            return result ?? null!;
            
        }

        public async Task<bool> UpdatePresupuesto(Presupuesto presupuesto, int userId)
        {
            var existingPresupuesto = await _context.Presupuestos.FirstOrDefaultAsync(p => p.UsuarioId == userId && p.Id == presupuesto.Id);

            if (existingPresupuesto == null)
            {
                return false;
            }

            existingPresupuesto.EstadoPresupuesto = presupuesto.EstadoPresupuesto;

            _context.Presupuestos.Update(existingPresupuesto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
