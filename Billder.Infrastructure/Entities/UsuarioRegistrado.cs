using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Billder.Infrastructure.Entities
{
    public partial class UsuarioRegistrado
    {
        public UsuarioRegistrado()
        {
            Clientes = new HashSet<Cliente>();
            Contratos = new HashSet<Contrato>();
            Empleados = new HashSet<Empleado>();
            Materials = new HashSet<Material>();
            Pagos = new HashSet<Pago>();
            PresupuestoEmpleados = new HashSet<PresupuestoEmpleado>();
            PresupuestoMaterials = new HashSet<PresupuestoMaterial>();
            Presupuestos = new HashSet<Presupuesto>();
            Trabajos = new HashSet<Trabajo>();
        }

        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Identificacion { get; set; } = null!;
        public string NroIdentificacion { get; set; } = null!;
        public DateTime? FechaNacimiento { get; set; }
        public string? Direccion { get; set; }
        public string? Ciudad { get; set; }
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
        public string? Telefono { get; set; }
        public string Password { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Cliente> Clientes { get; set; }
        [JsonIgnore]
        public virtual ICollection<Contrato> Contratos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Empleado> Empleados { get; set; }
        [JsonIgnore]
        public virtual ICollection<Material> Materials { get; set; }
        [JsonIgnore]
        public virtual ICollection<Pago> Pagos { get; set; }
        [JsonIgnore]
        public virtual ICollection<PresupuestoEmpleado> PresupuestoEmpleados { get; set; }
        [JsonIgnore]
        public virtual ICollection<PresupuestoMaterial> PresupuestoMaterials { get; set; }
        [JsonIgnore]
        public virtual ICollection<Presupuesto> Presupuestos { get; set; }
        [JsonIgnore]
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
