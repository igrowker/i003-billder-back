﻿using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository
{
    public class PresupuestoRepository : IPresupuestoRepository
    {
        private readonly AppDbContext _context;

        public PresupuestoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Presupuesto> CreatePresupuesto(Presupuesto presupuesto)
        {
            _context.Presupuestos.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;
        }

        public async Task<bool> DeletePresupuestoById(int id)
        {
            var presupuesto = await _context.Presupuestos.Include(p => p.Gastos)
                                                         .FirstOrDefaultAsync(p => p.Id == id);

            if (presupuesto == null)
            {
                return false;
            }

            _context.Presupuestos.Remove(presupuesto);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Presupuesto>> GetAllPresupuesto()
        {
            return await _context.Presupuestos.ToListAsync();
        }

        public async Task<Presupuesto> GetPresupuestoById(int id)
        {
            var result = await _context.Presupuestos.FindAsync(id);
            return result ?? null!;
            
        }

        public async Task<bool> UpdatePresupuesto(Presupuesto presupuesto)
        {
            var existingPresupuesto = await _context.Presupuestos.FindAsync(presupuesto.Id);

            if (existingPresupuesto == null)
            {
                return false;
            }
            existingPresupuesto.ClienteId = presupuesto.ClienteId;
            existingPresupuesto.FechaVencimiento = presupuesto.FechaVencimiento;
            existingPresupuesto.EstadoPresupuesto = presupuesto.EstadoPresupuesto;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
