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
        Task<IEnumerable<Empleado>> GetEmpleados();
        Task<Empleado> GetEmpleadoById(int id);
        Task<Empleado> CreateEmpleado(Empleado empleado);
        Task<bool> UpdateEmpleado (Empleado empleado);
        Task<bool> DeleteEmpleadoById (int id);
    }
}
