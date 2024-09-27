using Billder.Infrastructure.Entities;

namespace Billder.Application.Interfaces
{
    public interface IURegistradoInterface
    {
        Task<UsuarioRegistrado> CrearUsuarioRegistrado(UsuarioRegistrado usuario);
        Task<UsuarioRegistrado> UpdateUsuarioRegistrado(UsuarioRegistrado usuario);
        Task<UsuarioRegistrado> GetUsuarioRegistradoByID(int id);
        Task<int> DeleteUsuarioRegistrado(int id);
        Task<List<UsuarioRegistrado>> GetAllUsuariosRegistrados(int numeroPagina, string ordenamiento);
    }
}
