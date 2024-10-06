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
        Task<Cliente> CrearCliente(ClienteDTO clienteDTO);
        Task<Cliente> UpdateCliente(ClienteDTO clienteDTO);
        Task<Cliente> GetClienteByID(int id);
        Task<int> DeleteCliente(int id);
    }
}
