using Billder.Application.Interfaces;
using Billder.Infrastructure.Data;
using Billder.Infrastructure.Entities;
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
            return trabajo;
        }
        public async Task<Trabajo>GetTrabajoByID(int id)
        {
            return await _context.Trabajos.FindAsync(id);
        }
        public async Task<Trabajo> UpdateTrabajo( Trabajo trabajo)
        {
            return trabajo;
        }
    }
}
