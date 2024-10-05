using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IPresupuestoEmpleadoService
    {
        Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuestoEmpleadoAsync(int userId);
        Task<PresupuestoEmpleado> GetPresupuestoEmpleadoByIdAsync(int id, int userId);
        Task<PresupuestoEmpleado> CreatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado);
        Task<bool> UpdatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado, int userId);
        Task<bool> DeletePresupuestoEmpleadoByIdAsync(int id, int userId);
    }
}
