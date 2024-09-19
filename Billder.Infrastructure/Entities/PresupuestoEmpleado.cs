using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class PresupuestoEmpleado
    {
        public int Id { get; set; }
        public int? PresupuestoId { get; set; }
        public int? EmpleadoId { get; set; }
        public decimal? HorasTrabajadas { get; set; }
        public decimal? CostoHora { get; set; }

        public virtual Empleado? Empleado { get; set; }
        public virtual Presupuesto? Presupuesto { get; set; }
    }
}
