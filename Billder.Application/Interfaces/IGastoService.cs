using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public interface IGastoService
    {
        Task<Gasto> CrearGasto(Gasto gasto);
        Task<Gasto> GetGastoByID(int id);
        Task<Gasto> UpdateGasto(Gasto gasto);
        Task<int> DeleteGasto(int id);
        Task<List<GastoDTO>> GetHistorialDeGastoss(int clienteID, int numeroPagina, string ordenamiento);
    }

}
