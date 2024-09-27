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
        Task<IEnumerable<Material>> GetAllMaterialAsync();
        Task<Material> GetMaterialByIdAsync(int id);
        Task<Material> CreateMaterialAsync(Material material);
        Task<bool> UpdateMaterialAsync(Material material);
        Task<bool> DeleteMaterialByIdAsync(int id);
    }
}
