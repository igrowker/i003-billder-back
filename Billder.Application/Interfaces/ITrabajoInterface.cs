using Billder.Infrastructure.Entities;

namespace Billder.Application.Interfaces
{
    public interface ITrabajoInterface
    {
        Task<Trabajo> GetTrabajoByID(int id);
        Task<Trabajo> CrearTrabajo(Trabajo trabajo);
        Task<Trabajo> UpdateTrabajo(Trabajo trabajo);
        Task<int> DeleteTrabajo(int id);
        Task<List<Trabajo>> GetHistorialDeTrabajos(int usuarioID, int numeroPagina, string ordenamiento);
    }
}
