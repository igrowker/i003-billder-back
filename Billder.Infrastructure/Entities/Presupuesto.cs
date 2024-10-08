using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Billder.Infrastructure.Entities
{
    public partial class Presupuesto
    {
        public Presupuesto()
        {
            Trabajos = new HashSet<Trabajo>();
        }

        public int Id { get; set; }
        [Required]
        public int UsuarioId { get; set; }
        [Required]
        public int GastoId { get; set; }
        [Required]
        public int ClienteId { get; set; }
        public string EstadoPresupuesto { get; set; } = null!;

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Gasto Gasto { get; set; } = null!;
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
