using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class UsuarioDTO
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
        public string? Telefono { get; set; }
        public string? Imagen { get; set; }
        public string Password { get; set; } = null!;
    }
}
