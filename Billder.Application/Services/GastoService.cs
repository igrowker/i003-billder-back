using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class GastoService : IGastoService
    {
        private readonly IGastoRepository _gastoRepository;

        public GastoService(IGastoRepository gastoRepository)
        {
            _gastoRepository = gastoRepository;
        }

        public async Task<Gasto> CrearGasto(Gasto gasto)
        {
            return await _gastoRepository.CrearGastoRepository(gasto);
        }

        public async Task<Gasto> GetGastoByID(int id)
        {
            return await _gastoRepository.GetGastoByIDRepository(id);
        }

        public async Task<Gasto> UpdateGasto(Gasto gasto)
        {
            return await _gastoRepository.UpdateGastoRepository(gasto);
        }

        public async Task<int> DeleteGasto(int id)
        {
            return await _gastoRepository.DeleteGastoRepository(id);
        }

        public async Task<List<GastoDTO>> GetHistorialDeGastoss(int clienteID, int numeroPagina, string ordenamiento)
        {
            return await _gastoRepository.GetHistorialDeGastossRepository(clienteID, numeroPagina, ordenamiento);
        }
    }
}

