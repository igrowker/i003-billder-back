using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IClienteRepository
    {
        Task<Cliente> CrearClienteRepository(Cliente cliente);
        Task<Cliente> UpdateClienteRepository(Cliente cliente);
        Task<Cliente> GetClienteByIDRepository(int id);
        Task<int> DeleteClienteRepository(int id);
    }
}
