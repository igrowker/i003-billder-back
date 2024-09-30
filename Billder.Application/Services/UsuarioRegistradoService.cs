using Billder.Application.Custom;
using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Services
{

    public class UsuarioRegistradoService : IURegistradoInterface
    {
        private readonly IURegistradoRepository _uRegistradoRepository;
        private readonly Utilidades _utilidades; 

        public UsuarioRegistradoService(IURegistradoRepository uRegistradoRepository, Utilidades utilidades)
        {
            _uRegistradoRepository = uRegistradoRepository;
            _utilidades = utilidades;
        }

        public async Task<UsuarioRegistrado> CrearUsuarioRegistrado(UsuarioRegistrado usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException("Password is required", nameof(usuario.Password));
            usuario.Password = _utilidades.encriptarSHA256(usuario.Password);           
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
