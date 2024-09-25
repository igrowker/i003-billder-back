using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Empleado
    {
        public Empleado()
        {
            PresupuestoEmpleados = new HashSet<PresupuestoEmpleado>();
        }

        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string Identificacion { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public string? Puesto { get; set; }

        public virtual ICollection<PresupuestoEmpleado> PresupuestoEmpleados { get; set; }
    }
}
