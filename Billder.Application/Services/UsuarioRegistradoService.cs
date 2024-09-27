using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Services
{

    public class UsuarioRegistradoService : IURegistradoInterface
    {
        private readonly IURegistradoRepository _uRegistradoRepository;

        public UsuarioRegistradoService(IURegistradoRepository uRegistradoRepository)
        {
            _uRegistradoRepository = uRegistradoRepository;
        }

            public async Task<UsuarioRegistrado> CrearUsuarioRegistrado(UsuarioRegistrado usuario)
        {
            return await _uRegistradoRepository.CrearUsuarioRegistradoRepository(usuario);
        }

        public async Task<int> DeleteUsuarioRegistrado(int id)
        {
            return await _uRegistradoRepository.DeleteUsuarioRegistradoRepository(id);
        }

        public async Task<List<UsuarioRegistrado>> GetAllUsuariosRegistrados(int numeroPagina, string ordenamiento)
        {
            return await _uRegistradoRepository.GetAllUsuariosRegistradosRepository(numeroPagina, ordenamiento);
        }

        public async Task<UsuarioRegistrado> GetUsuarioRegistradoByID(int id)
        {
            return await _uRegistradoRepository.GetUsuarioRegistradoByIDRepository(id);
        }

        public async Task<UsuarioRegistrado> UpdateUsuarioRegistrado(UsuarioRegistrado usuario)
        {
            return await _uRegistradoRepository.UpdateUsuarioRegistradoRepository(usuario);
        }
    }
}
