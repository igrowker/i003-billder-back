using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Gasto
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PresupuestoId { get; set; }
        public string? Nombre { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? CostoHoraLaboral { get; set; }
        public decimal? HorasTrabajadas { get; set; }

        public virtual Presupuesto Presupuesto { get; set; } = null!;
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
    }
}
