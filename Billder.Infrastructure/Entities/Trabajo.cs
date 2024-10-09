using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Trabajo
    {
        public Trabajo()
        {
            Contratos = new HashSet<Contrato>();
            Pagos = new HashSet<Pago>();
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public int? ClienteId { get; set; }
        public int? PresupuestoId { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Imagen { get; set; }
        public string EstadoTrabajo { get; set; } = null!;

        public virtual Cliente? Cliente { get; set; }
        public virtual Presupuesto? Presupuesto { get; set; }
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
    }
}
