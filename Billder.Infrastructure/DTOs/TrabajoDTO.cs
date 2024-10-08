using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record TrabajoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string? Nombre { get; set; }
        public int? ClienteId { get; set; }
        public int? PresupuestoId { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public string? Imagen { get; set; }
        public string EstadoTrabajo { get; set; } = null!;
    }
}
