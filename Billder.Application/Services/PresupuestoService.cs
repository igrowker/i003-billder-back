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
    public class PresupuestoService : IPresupuestoService
    {
        private readonly IPresupuestoRepository _presupuestoRepository;
        public PresupuestoService(IPresupuestoRepository presupuestoRepository)
        {
            _presupuestoRepository = presupuestoRepository;
        }

        public async Task<Presupuesto> CreatePresupuestoAsync(Presupuesto presupuesto)
        {
            return await _presupuestoRepository.CreatePresupuesto(presupuesto);
        }

        public async Task<bool> DeletePresupuestoByIdAsync(int id, int userId)
        {
            return await _presupuestoRepository.DeletePresupuestoById(id, userId);
        }

        public async Task<IEnumerable<Presupuesto>> GetAllPresupuestosAsync(int userId)
        {
            return await _presupuestoRepository.GetAllPresupuesto(userId);
        }

        public async Task<Presupuesto> GetPresupuestoByIdAsync(int id, int userId)
        {
            return await _presupuestoRepository.GetPresupuestoById(id, userId);
        }

        public async Task<bool> UpdatePresupuestoAsync(Presupuesto presupuesto, int userId)
        {
            return await _presupuestoRepository.UpdatePresupuesto(presupuesto, userId);
        }
    }
}
