using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class UsuarioRegistrado
    {
        public UsuarioRegistrado()
        {
            Clientes = new HashSet<Cliente>();
            Contratos = new HashSet<Contrato>();
            Gastos = new HashSet<Gasto>();
            Pagos = new HashSet<Pago>();
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
        public string? Firma { get; set; }
        public string? Imagen { get; set; }

        public virtual ICollection<Cliente> Clientes { get; set; }
        public virtual ICollection<Contrato> Contratos { get; set; }
        public virtual ICollection<Gasto> Gastos { get; set; }
        public virtual ICollection<Pago> Pagos { get; set; }
        public virtual ICollection<Presupuesto> Presupuestos { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
