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

        public async Task<bool> DeletePresupuestoMaterialById(int id)
        {
            var presupuestoMaterial = await _context.PresupuestoMaterials.FindAsync(id);
            if (presupuestoMaterial == null)
            {
                return false;
            }
            _context.PresupuestoMaterials.Remove(presupuestoMaterial);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterial()
        {
            return await _context.PresupuestoMaterials.ToListAsync();
        }

        public async Task<PresupuestoMaterial> GetPresupuestoMaterialById(int id)
        {
            return await _context.PresupuestoMaterials.FindAsync(id);
        }

        public async Task<bool> UpdatePresupuestoMaterial(PresupuestoMaterial presupuestoMaterial)
        {
            var existingPresupuestoMaterial = await _context.PresupuestoMaterials.FindAsync(presupuestoMaterial.Id);
            if(existingPresupuestoMaterial == null)
            {

            return false; 
            }
            existingPresupuestoMaterial.PresupuestoID = presupuestoMaterial.PresupuestoID;
            existingPresupuestoMaterial.MaterialID = presupuestoMaterial.MaterialID;
            existingPresupuestoMaterial.Cantidad = presupuestoMaterial.Cantidad;
            existingPresupuestoMaterial.Costo = presupuestoMaterial.Costo;
            existingPresupuestoMaterial.Observacion = presupuestoMaterial.Observacion;

            _context.PresupuestoMaterials.Update(existingPresupuestoMaterial);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
