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
        Task<IEnumerable<Presupuesto>> GetAllPresupuesto();
        Task<Presupuesto> GetPresupuestoById(int id);
        Task<Presupuesto> CreatePresupuesto(Presupuesto presupuesto);
        Task<bool> UpdatePresupuesto(Presupuesto presupuesto);
        Task<bool> DeletePresupuestoById(int id);

    }
}
