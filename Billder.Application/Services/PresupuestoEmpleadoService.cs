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
    public class PresupuestoEmpleadoService : IPresupuestoEmpleadoService
    {
        private readonly IPresupuestoEmpleadoRepository _presupuestoEmpleadoRepository;
        public PresupuestoEmpleadoService(IPresupuestoEmpleadoRepository presupuestoEmpleadoRepository)
        {
            _presupuestoEmpleadoRepository = presupuestoEmpleadoRepository;
        }
        public async Task<PresupuestoEmpleado> CreatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado)
        {
            return await _presupuestoEmpleadoRepository.CreatePresupuestoEmpleado(presupuestoEmpleado);
        }

        public async Task<bool> DeletePresupuestoEmpleadoByIdAsync(int id)
        {
            return await _presupuestoEmpleadoRepository.DeletePresupuestoEmpleadoById(id);
        }

        public async Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuestoEmpleadoAsync()
        {
            return await _presupuestoEmpleadoRepository.GetAllPresupuesto();
        }

        public async Task<PresupuestoEmpleado> GetPresupuestoEmpleadoByIdAsync(int id)
        {
            return await _presupuestoEmpleadoRepository.GetPresupuestoEmpleadoById(id);
        }

        public async Task<bool> UpdatePresupuestoEmpleadoAsync(PresupuestoEmpleado presupuestoEmpleado)
        {
            return await _presupuestoEmpleadoRepository.UpdatePresupuestoEmpleado(presupuestoEmpleado);
        }
    }
}
