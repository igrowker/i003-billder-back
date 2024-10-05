using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IMaterialService
    {
        Task<IEnumerable<Material>> GetAllMaterialAsync(int userId);
        Task<Material> GetMaterialByIdAsync(int id, int userId);
        Task<Material> CreateMaterialAsync(Material material);
        Task<bool> UpdateMaterialAsync(Material material, int userId);
        Task<bool> DeleteMaterialByIdAsync(int id, int userId);
    }
}
