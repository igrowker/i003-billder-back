﻿using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Repository
{

    public class ClienteRepository : IClienteRepository
    {
    private readonly AppDbContext _context;

        public ClienteRepository(AppDbContext context) {
          _context = context;
        }
        
        public async Task<Cliente> CrearClienteRepository(Cliente cliente)
        {
            await _context.Clientes.AddAsync(cliente);
            await _context.SaveChangesAsync();
            return cliente;
        }

        public async Task<int> DeleteClienteRepository(int id)
        {
            var clienteEncontrado = await _context.Clientes.FindAsync(id);
            if (clienteEncontrado == null)
            {
                return 0;
            }
            _context.Clientes.Remove(clienteEncontrado);
            return await _context.SaveChangesAsync();
        }

        public async Task<Cliente> GetClienteByIDRepository(int id)
        {
            return await _context.Clientes.FindAsync(id);
        }

        public async Task<Cliente> UpdateClienteRepository(Cliente clienteRecibido)
        {
            var objetoCliente = await _context.Clientes.FindAsync(clienteRecibido.Id);

            objetoCliente.Descripcion = clienteRecibido.Descripcion;
            objetoCliente.Email = clienteRecibido.Email;
            objetoCliente.Identificacion = clienteRecibido.Identificacion;
            objetoCliente.NroIdentificacion = clienteRecibido.NroIdentificacion;
            objetoCliente.Pais = clienteRecibido.Pais;
            objetoCliente.Provincia = clienteRecibido.Provincia;
            objetoCliente.Ciudad = clienteRecibido.Ciudad;
            objetoCliente.Direccion = clienteRecibido.Direccion;
            objetoCliente.Nombre = clienteRecibido.Nombre;
            objetoCliente.Telefono = clienteRecibido.Telefono;

            await _context.SaveChangesAsync();
            return objetoCliente;
        }
    }
}
