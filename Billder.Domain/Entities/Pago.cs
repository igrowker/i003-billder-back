using Billder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Pago
    {
        public int Id { get; set; }
        public int TrabajoId { get; set; }
        public DateTime? FechaPago { get; set; }
        public float Monto { get; set; }
        public MetodoPago Metodo { get; set; }

    }
}
