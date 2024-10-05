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
        Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuesto(int userId);
        Task<PresupuestoEmpleado> GetPresupuestoEmpleadoById(int id, int userId);
        Task<PresupuestoEmpleado> CreatePresupuestoEmpleado(PresupuestoEmpleado presupuesto);
        Task<bool> UpdatePresupuestoEmpleado(PresupuestoEmpleado presupuesto, int userId);
        Task<bool> DeletePresupuestoEmpleadoById(int id, int userId);

    }
}
