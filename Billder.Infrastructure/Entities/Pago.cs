using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Pago
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int? TrabajoId { get; set; }
        public decimal? Monto { get; set; }
        public string Metodo { get; set; } = null!;
        public DateTime? FechaPago { get; set; }

        public virtual Trabajo? Trabajo { get; set; }
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
    }
}
