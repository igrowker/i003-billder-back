using Billder.Application.Interfaces;
using Billder.Application.Repository;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }

        public async Task<Cliente> CrearCliente(Cliente cliente)
        {
           return await _clienteRepository.CrearClienteRepository(cliente);
        }

        public async Task<int> DeleteCliente(int id, int userId)
        {
            return await _clienteRepository.DeleteClienteRepository(id, userId);
        }

        public async Task<Cliente> GetClienteByID(int id, int userId)
        {
            return await _clienteRepository.GetClienteByIDRepository(id, userId);
        }

        public async Task<Cliente> UpdateCliente(ClienteDTO clienteDTO, int userId)
        {
            var objetoCliente = await _clienteRepository.GetClienteByIDRepository(clienteDTO.Id, userId);

            objetoCliente.Descripcion = clienteDTO.Descripcion;
            objetoCliente.Email = clienteDTO.Email;
            objetoCliente.Identificacion = clienteDTO.Identificacion;
            objetoCliente.NroIdentificacion = clienteDTO.NroIdentificacion;
            objetoCliente.Pais = clienteDTO.Pais;
            objetoCliente.Provincia = clienteDTO.Provincia;
            objetoCliente.Ciudad = clienteDTO.Ciudad;
            objetoCliente.Direccion = clienteDTO.Direccion;
            objetoCliente.Nombre = clienteDTO.Nombre;
            objetoCliente.Telefono = clienteDTO.Telefono;

             await _clienteRepository.UpdateClienteRepository(objetoCliente, userId);
            return objetoCliente;
        }
    }
}
