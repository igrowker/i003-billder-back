using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IPresupuestoEmpleadoRepository
    {
        Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuesto();
        Task<PresupuestoEmpleado> GetPresupuestoEmpleadoById(int id);
        Task<PresupuestoEmpleado> CreatePresupuestoEmpleado(PresupuestoEmpleado presupuesto);
        Task<bool> UpdatePresupuestoEmpleado(PresupuestoEmpleado presupuesto);
        Task<bool> DeletePresupuestoEmpleadoById(int id);

    }
}
