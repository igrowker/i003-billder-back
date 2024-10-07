using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Gasto
    {
        public Gasto()
        {
            Presupuestos = new HashSet<Presupuesto>();
        }

        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? CostoHoraLaboral { get; set; }
        public decimal? HorasTrabajadas { get; set; }

        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
        public virtual ICollection<Presupuesto> Presupuestos { get; set; }
    }
}
