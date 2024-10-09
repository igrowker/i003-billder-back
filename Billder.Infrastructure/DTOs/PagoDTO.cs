using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class PagoDTO
    {
        public int Id { get; set; }
        public int? TrabajoId { get; set; }
        public decimal? Monto { get; set; }
        public string Metodo { get; set; } = null!;
        public DateTime? FechaPago { get; set; }
    }
}
