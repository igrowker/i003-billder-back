using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Billder.Application.Custom;
using Billder.Infrastructure.Entities;
using Billder.Infrastructure.DTOs;
using Microsoft.AspNetCore.Authorization;
using Billder.Infrastructure.Data;


namespace Billder.API.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccesoController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly Utilidades _utilidades;
        public AccesoController(AppDbContext context, Utilidades utilidades)
        {
            _utilidades = utilidades;
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDTO objeto)
        {
            var usuarioEncontrado = await _context.UsuarioRegistrados
                                    .Where(u =>
                                    u.Email == objeto.Email &&
                                    u.Password == _utilidades.encriptarSHA256(objeto.Password)
                                    ).FirstOrDefaultAsync();

            if(usuarioEncontrado == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false, token = "" });
            }
            else
            {
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true, token = _utilidades.generarJWT(usuarioEncontrado) });
            }

        }
    }
}
