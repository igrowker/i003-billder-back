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
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly AppDbContext _context;
        public EmpleadoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Empleado>> GetEmpleados(int userId)
        {
            return await _context.Empleados.Where(e => e.UsuarioId == userId).ToListAsync();
        }

        public async Task<Empleado> GetEmpleadoById(int id, int userId)
        {
            var result = await _context.Empleados.FirstOrDefaultAsync(e => e.UsuarioId == userId && e.Id == id);
            return result ?? null!;
        }

        public async Task<Empleado> CreateEmpleado(Empleado empleado)
        {
            _context.Empleados.Add(empleado);
            await _context.SaveChangesAsync();
            return empleado;
        }

        public async Task<bool> UpdateEmpleado(Empleado empleado, int userId)
        {
            var existingEmpleado = await _context.Empleados.FirstOrDefaultAsync(e => e.UsuarioId == userId && e.Id == empleado.Id);
            if (existingEmpleado == null)
            {
                return false;
            }
            existingEmpleado.Fullname = empleado.Fullname;
            existingEmpleado.Identificacion = empleado.Identificacion;
            existingEmpleado.NroIdentificacion = empleado.NroIdentificacion;
            existingEmpleado.Puesto = empleado?.Puesto;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteEmpleadoById(int id, int userId)
        {
            var material = await _context.Empleados.FirstOrDefaultAsync(e => e.UsuarioId == userId && e.Id == id);
            if (material == null)
            {
                return false;
            }
            _context.Empleados.Remove(material);
            await _context.SaveChangesAsync();
            return true;
        }

        
    }
}
