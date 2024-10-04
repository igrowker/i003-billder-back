using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IContratoRepository
    {
        Task<Contrato> CrearContratoRepository(Contrato contrato);
        Task<Contrato> GetContratoByIDRepository(int id);
        Task<int> DeleteContratoRepository(int id);
        Task<Contrato> UpdateContratoRepository(Contrato contrato);
        Task<List<Contrato>> GetHistorialDeContratosRepository(int clienteID, int numeroPagina);
    }
}
