using Billder.Application.Repository.Interfaces;
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
    public class PresupuestoEmpleadoRepository : IPresupuestoEmpleadoRepository
    {
        private readonly AppDbContext _context;
        public PresupuestoEmpleadoRepository (AppDbContext context)
        {
            _context = context;
        }
        public async Task<PresupuestoEmpleado> CreatePresupuestoEmpleado(PresupuestoEmpleado presupuesto)
        {
            _context.PresupuestoEmpleados.Add(presupuesto);
            await _context.SaveChangesAsync();
            return presupuesto;

        }

        public async Task<bool> DeletePresupuestoEmpleadoById(int id)
        {
            var presupuestoEmpleado = await _context.PresupuestoEmpleados.FindAsync(id);
            if(presupuestoEmpleado == null)
            {
                return false;
            }

            _context.PresupuestoEmpleados.Remove(presupuestoEmpleado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuesto()
        {
            return await _context.PresupuestoEmpleados.ToListAsync();
        }

        public async Task<PresupuestoEmpleado> GetPresupuestoEmpleadoById(int id)
        {
            return await _context.PresupuestoEmpleados.FindAsync(id);
        }

        public async Task<bool> UpdatePresupuestoEmpleado(PresupuestoEmpleado presupuestoEmpleado)
        {
            var existingPresupuestoEmpleado = await _context.PresupuestoEmpleados.FindAsync(presupuestoEmpleado.Id);

            if (existingPresupuestoEmpleado == null)
            {
                return false;
            }

            existingPresupuestoEmpleado.PresupuestoId = presupuestoEmpleado.PresupuestoId;
            existingPresupuestoEmpleado.EmpleadoId = presupuestoEmpleado.EmpleadoId;
            existingPresupuestoEmpleado.HorasTrabajadas = presupuestoEmpleado.HorasTrabajadas;
            existingPresupuestoEmpleado.CostoHora = presupuestoEmpleado.CostoHora;

            _context.PresupuestoEmpleados.Update(presupuestoEmpleado);
            await _context.SaveChangesAsync();
            return true;


        }
    }
}
