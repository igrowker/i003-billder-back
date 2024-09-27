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
        Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterialAsync();
        Task<PresupuestoMaterial> GetPresupuestoMaterialByIdAsync(int id);
        Task<PresupuestoMaterial> CreatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial);
        Task<bool> UpdatePresupuestoMaterialAsync(PresupuestoMaterial presupuestoMaterial);
        Task<bool> DeletePresupuestoMaterialByIdAsync(int id);
    }
}
