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

        public async Task<bool> DeletePresupuestoEmpleadoById(int id, int userId)
        {
            var presupuestoEmpleado = await _context.PresupuestoEmpleados.FirstOrDefaultAsync(pe => pe.UsuarioId == userId && pe.Id == id);
            if(presupuestoEmpleado == null)
            {
                return false;
            }

            _context.PresupuestoEmpleados.Remove(presupuestoEmpleado);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PresupuestoEmpleado>> GetAllPresupuesto(int userId)
        {
            return await _context.PresupuestoEmpleados.Where(pe => pe.UsuarioId == userId).ToListAsync();
        }

        public async Task<PresupuestoEmpleado> GetPresupuestoEmpleadoById(int id, int userId)
        {
            var result = await _context.PresupuestoEmpleados.FirstOrDefaultAsync(pe => pe.UsuarioId == userId && pe.Id == id);
            return result ?? null!;
        }

        public async Task<bool> UpdatePresupuestoEmpleado(PresupuestoEmpleado presupuestoEmpleado, int userId)
        {
            var existingPresupuestoEmpleado = await _context.PresupuestoEmpleados.FirstOrDefaultAsync(pe => pe.UsuarioId == userId && pe.Id == presupuestoEmpleado.Id);

            if (existingPresupuestoEmpleado == null)
            {
                return false;
            }

            existingPresupuestoEmpleado.PresupuestoId = presupuestoEmpleado.PresupuestoId;
            existingPresupuestoEmpleado.EmpleadoId = presupuestoEmpleado.EmpleadoId;
            existingPresupuestoEmpleado.HorasTrabajadas = presupuestoEmpleado.HorasTrabajadas;
            existingPresupuestoEmpleado.CostoHora = presupuestoEmpleado.CostoHora;

            await _context.SaveChangesAsync();
            return true;


        }
    }
}
