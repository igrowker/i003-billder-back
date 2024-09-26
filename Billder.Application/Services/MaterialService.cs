using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class MaterialService : IMaterialService
    {
        private readonly IMaterialRepository _materialRepository;
        public MaterialService(IMaterialRepository materialRepository)
        {
            _materialRepository = materialRepository;
        }
        public async Task<Material> CreateMaterialAsync(Material material)
        {
            return await _materialRepository.CreateMaterial(material);
        }

        public async Task<bool> DeleteMaterialByIdAsync(int id)
        {
            return await _materialRepository.DeleteMaterialById(id);
        }

        public async Task<IEnumerable<Material>> GetAllMaterialAsync()
        {
            return await _materialRepository.GetAllMaterial();
        }

        public async Task<Material> GetMaterialByIdAsync(int id)
        {
            return await _materialRepository.GetMaterialById(id);
        }

        public async Task<bool> UpdateMaterialAsync(Material material)
        {
            return await _materialRepository.UpdateMaterial(material);
        }
    }
}
