using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly IEmpleadoRepository _empleadoRepository;
        public EmpleadoService(IEmpleadoRepository empleadoRepository)
        {
            _empleadoRepository = empleadoRepository;
        }

        public async Task<Empleado> CreateEmpleadoAsync(Empleado material)
        {
            return await _empleadoRepository.CreateEmpleado(material);
        }

        public async Task<bool> DeleteEmpleadoByIdAsync(int id, int userId)
        {
            return await _empleadoRepository.DeleteEmpleadoById(id, userId);
        }

        public async Task<IEnumerable<Empleado>> GetAllEmpleadoAsync(int userId)
        {
            return await _empleadoRepository.GetEmpleados(userId);
        }

        public async Task<Empleado> GetEmpleadoByIdAsync(int id, int userId)
        {
            return await _empleadoRepository.GetEmpleadoById(id, userId);
        }

        public async Task<bool> UpdateEmpleadoAsync(Empleado material, int userId)
        {
            return await _empleadoRepository.UpdateEmpleado(material, userId);
        }
    }
}
