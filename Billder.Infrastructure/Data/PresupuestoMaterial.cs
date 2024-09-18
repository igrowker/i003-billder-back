using System;
using System.Collections.Generic;

namespace Billder.Data
{
    public partial class PresupuestoMaterial
    {
        public int Id { get; set; }
        public int? PresupuestoID { get; set; }
        public int? MaterialID { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Costo { get; set; }
        public string? Observacion { get; set; }

        public virtual Material? Material { get; set; }
        public virtual Presupuesto? Presupuesto { get; set; }
    }
}
