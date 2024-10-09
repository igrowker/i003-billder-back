using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Interfaces
{
    public interface IClienteService
    {
        Task<Cliente> CrearCliente(Cliente cliente);
        Task<Cliente> UpdateCliente(ClienteDTO clienteDTO, int userId);
        Task<Cliente> GetClienteByID(int id, int userId);
        Task<int> DeleteCliente(int id, int userId);
    }
}
