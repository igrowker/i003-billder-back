
using Billder.Application.Custom;
using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Services;
    public class TrabajoService : ITrabajoInterface
    {
        private readonly ITrabajoRepository _trabajoRepository;

        public TrabajoService(ITrabajoRepository trabajoRepository)
        {
            _trabajoRepository = trabajoRepository;
        }

        public async Task<Trabajo> CrearTrabajo(Trabajo trabajo)
        {
            return await _trabajoRepository.CrearTrabajoRepository(trabajo);

        }

        public async Task<Trabajo> GetTrabajoByID(int id, int userId)
        {
            return await _trabajoRepository.GetTrabajoByIDRepository(id, userId);
        }

        public async Task<Trabajo> UpdateTrabajo(Trabajo trabajo, int userId)
        {
            return await _trabajoRepository.UpdateTrabajoRepository(trabajo, userId);
        }
        public async Task<int> DeleteTrabajo(int id, int userId)
        {
            return await _trabajoRepository.DeleteTrabajoRepository(id, userId);
        }
        public async Task<Paginacion<TrabajoDTO>> GetHistorialDeTrabajos(int clienteID, int userId, int numeroPagina, string ordenamiento)
        {
            return await _trabajoRepository.GetHistorialDeTrabajosRepository(clienteID, userId, numeroPagina,ordenamiento);
        }
    }
