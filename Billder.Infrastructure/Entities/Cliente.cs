using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Cliente
    {
        public Cliente()
        {
            Trabajos = new HashSet<Trabajo>();
        }

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
        public DateTime? FechaAlta { get; set; }

        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
