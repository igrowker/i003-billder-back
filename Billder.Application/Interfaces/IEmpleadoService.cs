using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IEmpleadoService
    {
        Task<IEnumerable<Empleado>> GetAllEmpleadoAsync();
        Task<Empleado> GetEmpleadoByIdAsync(int id);
        Task<Empleado> CreateEmpleadoAsync(Empleado material);
        Task<bool> UpdateEmpleadoAsync(Empleado material);
        Task<bool> DeleteEmpleadoByIdAsync(int id);
    }
}
