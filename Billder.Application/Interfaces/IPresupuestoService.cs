using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IPresupuestoService
    {
        Task<IEnumerable<Presupuesto>> GetAllPresupuestosAsync();
        Task<Presupuesto> GetPresupuestoByIdAsync(int id);
        Task<Presupuesto> CreatePresupuestoAsync(Presupuesto presupuesto);
        Task<bool> UpdatePresupuestoAsync(Presupuesto presupuesto);
        Task<bool> DeletePresupuestoByIdAsync(int id);
    }
}
