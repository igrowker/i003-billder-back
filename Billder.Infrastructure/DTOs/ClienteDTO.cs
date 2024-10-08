using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record ClienteDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Identificacion { get; set; } = null!;
        public string? NroIdentificacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
        public string? Imagen { get; set; }
        public DateTime? FechaAlta { get; set; }
    }
}
