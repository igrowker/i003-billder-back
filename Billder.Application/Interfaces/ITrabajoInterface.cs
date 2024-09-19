using Billder.Infrastructure.Entities;

namespace Billder.Application.Interfaces
{
    public interface ITrabajoInterface
    {
        Task<Trabajo> GetTrabajoByID(int id);
        Task<Trabajo> CrearTrabajo(Trabajo trabajo);
        Task<Trabajo> UpdateTrabajo(Trabajo trabajo);
    }
}
