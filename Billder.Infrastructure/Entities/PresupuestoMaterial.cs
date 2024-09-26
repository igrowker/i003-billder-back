using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Billder.Infrastructure.Entities
{
    public partial class PresupuestoMaterial
    {
        public int Id { get; set; }
        public int? PresupuestoID { get; set; }
        public int? MaterialID { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Costo { get; set; }
        public string? Observacion { get; set; }

        [JsonIgnore]
        public virtual Material? Material { get; set; }
        [JsonIgnore]
        public virtual Presupuesto? Presupuesto { get; set; }
    }
}
