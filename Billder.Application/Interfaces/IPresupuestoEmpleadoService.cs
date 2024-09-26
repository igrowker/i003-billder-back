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
        Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuestoEmpleadoAsync();
        Task<PresupuestoEmpleado> GetPresupuestoEmpleadoByIdAsync(int id);
        Task<PresupuestoEmpleado> CreatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado);
        Task<bool> UpdatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado);
        Task<bool> DeletePresupuestoEmpleadoByIdAsync(int id);
    }
}
