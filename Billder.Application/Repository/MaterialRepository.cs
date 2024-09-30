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
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;
        public MaterialRepository(AppDbContext context) 
        {
            _context = context;   
        }
        public async Task<Material> CreateMaterial(Material material)
        {
            _context.Materials.Add(material);
            await _context.SaveChangesAsync();
            return material;
        }

        public async Task<bool> DeleteMaterialById(int id)
        {
            var material = await _context.Materials.FindAsync(id);
            if (material == null)
            {
                return false;
            }
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Material>> GetAllMaterial()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<Material> GetMaterialById(int id)
        {
            var result = await _context.Materials.FindAsync(id);
            return result ?? null!;
        }

        public async Task<bool> UpdateMaterial(Material material)
        {
            var existingMaterial = await _context.Materials.FindAsync(material.Id);
            if (existingMaterial == null)
            {
                return false;
            }

            existingMaterial.Descripcion = material.Descripcion;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
