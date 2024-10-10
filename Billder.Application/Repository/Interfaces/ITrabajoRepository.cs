using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Billder.Infrastructure.DTOs;
using Billder.Application.Custom;
namespace Billder.Application.Repository.Interfaces
{
    public interface ITrabajoRepository
    {
        Task<Trabajo> CrearTrabajoRepository (Trabajo trabajo);
        Task<Trabajo> UpdateTrabajoRepository(Trabajo trabajo, int userId);
        Task<Trabajo> GetTrabajoByIDRepository(int id, int userId);
        Task<int> DeleteTrabajoRepository(int id, int userId);
        Task<Paginacion<TrabajoDTO>> GetHistorialDeTrabajosRepository(int usuarioID, int userId, int numeroPagina, string ordenamiento);
    }
}
