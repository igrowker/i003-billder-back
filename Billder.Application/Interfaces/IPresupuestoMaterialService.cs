using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IPresupuestoMaterialService
    {
        Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterialAsync(int userId);
        Task<PresupuestoMaterial> GetPresupuestoMaterialByIdAsync(int id, int userId);
        Task<PresupuestoMaterial> CreatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial);
        Task<bool> UpdatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial, int userId);
        Task<bool> DeletePresupuestoMaterialByIdAsync(int id, int userId);
    }
}
