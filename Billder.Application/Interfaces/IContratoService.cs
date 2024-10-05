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
        Task<Contrato> GetContratoByID(int id);
        Task<int> DeleteContrato(int id);
        Task<Contrato> UpdateContrato(Contrato contrato);
        Task<List<Contrato>> GetHistorialDeContratos(int usuarioID, int numeroPagina);
    }
}
