using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IPresupuestoRepository
    {
        Task<IEnumerable<Presupuesto>> GetAllPresupuesto(int userId);
        Task<Presupuesto> GetPresupuestoById(int id, int userId);
        Task<Presupuesto> CreatePresupuesto(Presupuesto presupuesto);
        Task<bool> UpdatePresupuesto(Presupuesto presupuesto, int userId);
        Task<bool> DeletePresupuestoById(int id, int userId);

    }
}
