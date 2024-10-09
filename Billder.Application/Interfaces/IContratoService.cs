using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
   public interface IContratoService
    {
        Task<Contrato> CrearContrato(Contrato contrato);
        Task<Contrato> GetContratoByID(int id, int userId);
        Task<int> DeleteContrato(int id, int userId);
        Task<Contrato> UpdateContrato(Contrato contrato, int userId);
        Task<List<Contrato>> GetHistorialDeContratos(int usuarioID, int numeroPagina);
    }
}
