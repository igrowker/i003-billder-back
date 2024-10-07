using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Presupuesto
    {
        public Presupuesto()
        {
            Contratos = new HashSet<Contrato>();
            Trabajos = new HashSet<Trabajo>();
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int GastoId { get; set; }
        public int ContratoId { get; set; }
        public int ClienteId { get; set; }
        public string EstadoPresupuesto { get; set; } = null!;

        public virtual Cliente Cliente { get; set; } = null!;
        public virtual Gasto Gasto { get; set; } = null!;
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
