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
        Task<IEnumerable<Presupuesto>> GetAllPresupuestosAsync(int userId);
        Task<Presupuesto> GetPresupuestoByIdAsync(int id, int userId);
        Task<Presupuesto> CreatePresupuestoAsync(Presupuesto presupuesto);
        Task<bool> UpdatePresupuestoAsync(Presupuesto presupuesto, int userId);
        Task<bool> DeletePresupuestoByIdAsync(int id, int userId);
    }
}
