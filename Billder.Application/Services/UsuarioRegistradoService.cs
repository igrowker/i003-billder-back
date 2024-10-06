using Billder.Application.Custom;
using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.DTOs;
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
            //usuario.Password = _utilidades.encriptarSHA256(usuario.Password);
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

        public Task<bool> UpdatePasword(UsuarioRegistrado usuario, ChangePasswordDTO request)
        {
            if (string.IsNullOrEmpty(request.newPassword))
            {
                throw new ArgumentException("La password no puede estar vacia", nameof(request.newPassword));
            }
            try
            {
                if(!VerifyPassword(usuario.Password, request.oldPassword))
                {
                    throw new ArgumentException("La password actual no es correcta");
                }
                usuario.Password = _utilidades.encriptarSHA256(request.newPassword);
                return UpdateUsuarioRegistrado(usuario);
            }
            catch (Exception)
            {

                throw;
            }
        }
        private bool VerifyPassword(string password, string oldPassword)
        {
            if (password != _utilidades.encriptarSHA256(oldPassword))
            {
                return false;
            }
            return true;

        }

        public async Task<bool> UpdateUsuarioRegistrado(UsuarioRegistrado usuario)
        {
            return await _uRegistradoRepository.UpdateUsuarioRegistradoRepository(usuario);
        }
    }
}
