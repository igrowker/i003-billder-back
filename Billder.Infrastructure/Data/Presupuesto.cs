using System;
using System.Collections.Generic;

namespace Billder.Data
{
    public partial class Presupuesto
    {
        public Presupuesto()
        {
            PresupuestoEmpleados = new HashSet<PresupuestoEmpleado>();
            PresupuestoMaterials = new HashSet<PresupuestoMaterial>();
            Trabajos = new HashSet<Trabajo>();
        }

        public int Id { get; set; }
        public string EstadoPresupuesto { get; set; } = null!;

        public virtual ICollection<PresupuestoEmpleado> PresupuestoEmpleados { get; set; }
        public virtual ICollection<PresupuestoMaterial> PresupuestoMaterials { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
