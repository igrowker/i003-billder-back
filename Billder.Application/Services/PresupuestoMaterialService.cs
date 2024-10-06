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
    public class PresupuestoMaterialService : IPresupuestoMaterialService
    {
        private readonly IPresupuestoMaterialRepository _presupuestoMaterialRepository;
        public PresupuestoMaterialService(IPresupuestoMaterialRepository presupuestoMaterialRepository)
        {
            _presupuestoMaterialRepository = presupuestoMaterialRepository;
        }

        public async Task<PresupuestoMaterial> CreatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial)
        {
            return await _presupuestoMaterialRepository.CreatePresupuestoMaterial(presupuestoMaterial);
        }

        public async Task<bool> DeletePresupuestoMaterialByIdAsync(int id, int userId)
        {
            return await _presupuestoMaterialRepository.DeletePresupuestoMaterialById(id, userId);
        }

        public async Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterialAsync(int userId)
        {
            return await _presupuestoMaterialRepository.GetAllPresupuestoMaterial(userId);
        }

        public async Task<PresupuestoMaterial> GetPresupuestoMaterialByIdAsync(int id, int userId)
        {
            return await _presupuestoMaterialRepository.GetPresupuestoMaterialById(id, userId);
        }

        public async Task<bool> UpdatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial, int userId)
        {
            return await _presupuestoMaterialRepository.UpdatePresupuestoMaterial(presupuestoMaterial, userId);
        }
    }
}
