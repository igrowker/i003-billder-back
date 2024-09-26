using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class UsuarioRegistrado
    {
        public int Id { get; set; }
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
        public string Password { get; set; } = null!;
    }
}
