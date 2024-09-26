using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface IURegistradoRepository
    {
        Task<UsuarioRegistrado> CrearUsuarioRegistradoRepository(UsuarioRegistrado usuario);
        Task<UsuarioRegistrado> UpdateUsuarioRegistradoRepository(UsuarioRegistrado usuario);
        Task<UsuarioRegistrado> GetUsuarioRegistradoByIDRepository(int id);
        Task<int> DeleteUsuarioRegistradoRepository(int id);
        Task<List<UsuarioRegistrado>> GetAllUsuariosRegistradosRepository(int usuarioID, int numeroPagina);
    }
}
