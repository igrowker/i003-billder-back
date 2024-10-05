using Billder.Infrastructure.DTOs;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Interfaces
{
    public interface IURegistradoInterface
    {
        Task<UsuarioRegistrado> CrearUsuarioRegistrado(UsuarioRegistrado usuario);
        Task<bool> UpdateUsuarioRegistrado(UsuarioRegistrado usuario);
        Task<UsuarioRegistrado> GetUsuarioRegistradoByID(int id);
        Task<int> DeleteUsuarioRegistrado(int id);
        Task<List<UsuarioRegistrado>> GetAllUsuariosRegistrados(int numeroPagina, string ordenamiento);
        Task<bool> UpdatePasword(UsuarioRegistrado usuario, ChangePasswordDTO request);
    }
}
