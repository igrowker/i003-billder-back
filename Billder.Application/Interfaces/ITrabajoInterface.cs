using Billder.Application.Custom;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Interfaces
{
    public interface ITrabajoInterface
    {
        Task<Trabajo> GetTrabajoByID(int id, int userId);
        Task<Trabajo> CrearTrabajo(Trabajo trabajo);
        Task<Trabajo> UpdateTrabajo(Trabajo trabajo, int userId);
        Task<int> DeleteTrabajo(int id, int userId);
        Task<Paginacion<TrabajoDTO>> GetHistorialDeTrabajos(int usuarioID, int userId, int numeroPagina, string ordenamiento);
    }
}
