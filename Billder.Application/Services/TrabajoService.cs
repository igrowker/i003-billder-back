
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

        public async Task<Trabajo> GetTrabajoByID(int id)
        {
            return await _trabajoRepository.GetTrabajoByIDRepository(id);
        }

        public async Task<Trabajo> UpdateTrabajo(Trabajo trabajo)
        {
            return await _trabajoRepository.UpdateTrabajoRepository(trabajo);
        }
        public async Task<int> DeleteTrabajo(int id)
        {
            return await _trabajoRepository.DeleteTrabajoRepository(id);
        }
        public async Task<List<TrabajoDTO>> GetHistorialDeTrabajos(int clienteID, int numeroPagina, string ordenamiento)
        {
            return await _trabajoRepository.GetHistorialDeTrabajosRepository(clienteID,numeroPagina,ordenamiento);
        }
    }
