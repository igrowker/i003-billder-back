using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IMaterialRepository
    {
        Task<IEnumerable<Material>> GetAllMaterial();
        Task<Material> GetMaterialById(int id);
        Task<Material> CreateMaterial(Material material);
        Task<bool> UpdateMaterial(Material material);
        Task<bool> DeleteMaterialById(int id);
    }
}
