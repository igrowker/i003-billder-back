using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IPresupuestoMaterialRepository
    {
        Task<IEnumerable<PresupuestoMaterial>> GetAllPresupuestoMaterial();
        Task<PresupuestoMaterial> GetPresupuestoMaterialById(int id);
        Task<PresupuestoMaterial> CreatePresupuestoMaterial(PresupuestoMaterial presupuestoMaterial);
        Task<bool> UpdatePresupuestoMaterial(PresupuestoMaterial presupuestoMaterial);
        Task<bool> DeletePresupuestoMaterialById(int id);
    }
}
