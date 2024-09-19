﻿using Billder.Application.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository
{
    public class TrabajoRepository : ITrabajoInterface
    {
        private readonly AppDbContext _context;

        public TrabajoRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Trabajo>CrearTrabajo(Trabajo trabajo)
        {
            try
            {
                await _context.Trabajos.AddAsync(trabajo);
                _context.SaveChangesAsync();
                return trabajo;
            }
            catch (DbUpdateException ex)
            {
                throw new Exception("Ocurrio un error al crear el trabajo", ex);
            }

        }
        public async Task<Trabajo>GetTrabajoByID(int id)
        {
            try
            {
                return await _context.Trabajos.FindAsync(id);

            }
            catch (Exception ex) {
                throw new Exception("Ocurrio un error al obtener el trabajo por ID ", ex);
            }
        }
        public async Task<Trabajo> UpdateTrabajo( Trabajo trabajoRecibido)
        {
            try
            {
                var objetoTrabajo = await _context.Trabajos.FindAsync(trabajoRecibido.Id);

                if (objetoTrabajo != null)
                {
                    objetoTrabajo.Nombre = trabajoRecibido.Nombre;
                    objetoTrabajo.Descripcion = trabajoRecibido.Descripcion;
                    objetoTrabajo.Fecha = trabajoRecibido.Fecha;
                    objetoTrabajo.EstadoTrabajo = trabajoRecibido.EstadoTrabajo;
                }

                await _context.SaveChangesAsync();
                return objetoTrabajo;
            }
            catch (DbUpdateException ex) 
            {
                throw new Exception("Ocurrio un error al actualizar el trabajo", ex);
            }

        }
    }
}
