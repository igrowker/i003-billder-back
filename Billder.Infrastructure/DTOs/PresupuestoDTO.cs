using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class PresupuestoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int ClienteId { get; set; }
        public DateTime? FechaVencimiento { get; set; }
        public string EstadoPresupuesto { get; set; } = null!;
    }
}
