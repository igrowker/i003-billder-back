using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Billder.Infrastructure.Entities
{
    public partial class PresupuestoEmpleado
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int EmpleadoId { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public decimal? CostoHora { get; set; }

        [JsonIgnore]
        public virtual Empleado Empleado { get; set; } = null!;
        [JsonIgnore]
        public virtual Presupuesto Presupuesto { get; set; } = null!;
    }
}
