using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record ContratoDTO
    {
        public int Id { get; set; }
        public int? TrabajoId { get; set; }
        public int? PresupuestoId { get; set; }
        public string? Condiciones { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaFirma { get; set; }
        public string Estado { get; set; } = null!;
        public string? FirmaDigital { get; set; }
    }
}
