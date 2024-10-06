using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IEmpleadoRepository
    {
        Task<IEnumerable<Empleado>> GetEmpleados(int userId);
        Task<Empleado> GetEmpleadoById(int id, int userId);
        Task<Empleado> CreateEmpleado(Empleado empleado);
        Task<bool> UpdateEmpleado (Empleado empleado, int userId);
        Task<bool> DeleteEmpleadoById (int id, int userId);
    }
}
