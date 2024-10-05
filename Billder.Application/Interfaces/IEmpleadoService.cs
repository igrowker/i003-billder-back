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
        Task<IEnumerable<Empleado>> GetAllEmpleadoAsync(int userId);
        Task<Empleado> GetEmpleadoByIdAsync(int id, int userId);
        Task<Empleado> CreateEmpleadoAsync(Empleado material);
        Task<bool> UpdateEmpleadoAsync(Empleado material, int userId);
        Task<bool> DeleteEmpleadoByIdAsync(int id, int userId);
    }
}
