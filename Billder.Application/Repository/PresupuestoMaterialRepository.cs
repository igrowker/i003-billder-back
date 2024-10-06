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
    public class PresupuestoMaterialRepository : IPresupuestoMaterialRepository
    {
        private readonly AppDbContext _context;
        public PresupuestoMaterialRepository (AppDbContext context)
        {
            _context = context;
        }
        public async Task<PresupuestoMaterial> CreatePresupuestoMaterial(PresupuestoMaterial presupuestoMaterial)
        {
            _context.PresupuestoMaterials.Add(presupuestoMaterial);
            await _context.SaveChangesAsync();
            return presupuestoMaterial;
        }

        public async Task<bool> DeletePresupuestoMaterialById(int id, int userId)
        {
            var presupuestoMaterial = await _context.PresupuestoMaterials.FirstOrDefaultAsync(pm => pm.UsuarioId == userId && pm.Id == id);
            if (presupuestoMaterial == null)
            {
                return false;
            }
            _context.PresupuestoMaterials.Remove(presupuestoMaterial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterial(int userId)
        {
            return await _context.PresupuestoMaterials.Where(pm => pm.UsuarioId == userId).ToListAsync();
        }

        public async Task<PresupuestoMaterial> GetPresupuestoMaterialById(int id, int userId)
        {
            var result = await _context.PresupuestoMaterials.FirstOrDefaultAsync(pm => pm.UsuarioId == userId && pm.Id == id);
            return result ?? null!;
        }

        public async Task<bool> UpdatePresupuestoMaterial(PresupuestoMaterial presupuestoMaterial, int userId)
        {
            var existingPresupuestoMaterial = await _context.PresupuestoMaterials.FirstOrDefaultAsync(pm => pm.UsuarioId == userId && pm.Id == presupuestoMaterial.Id);
            if (existingPresupuestoMaterial == null)
            {

            return false; 
            }
            existingPresupuestoMaterial.PresupuestoID = presupuestoMaterial.PresupuestoID;
            existingPresupuestoMaterial.MaterialID = presupuestoMaterial.MaterialID;
            existingPresupuestoMaterial.Cantidad = presupuestoMaterial.Cantidad;
            existingPresupuestoMaterial.Costo = presupuestoMaterial.Costo;
            existingPresupuestoMaterial.Observacion = presupuestoMaterial.Observacion;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
