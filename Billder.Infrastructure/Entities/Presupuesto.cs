using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Billder.Infrastructure.Entities
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

        [JsonIgnore]
        public virtual ICollection<PresupuestoEmpleado> PresupuestoEmpleados { get; set; }
        [JsonIgnore]
        public virtual ICollection<PresupuestoMaterial> PresupuestoMaterials { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
