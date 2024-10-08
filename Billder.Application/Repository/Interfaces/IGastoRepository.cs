using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public interface IGastoRepository
    {
        Task<Gasto> CrearGastoRepository(Gasto gasto);
        Task<Gasto> GetGastoByIDRepository(int id);
        Task<Gasto> UpdateGastoRepository(Gasto gasto);
        Task<int> DeleteGastoRepository(int id);
        Task<List<GastoDTO>> GetHistorialDeGastossRepository(int clienteID, int numeroPagina, string ordenamiento);
    }

}
