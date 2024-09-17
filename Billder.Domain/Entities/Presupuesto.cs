using Billder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Presupuesto
    {
        public int Id { get; set; }
        public EstadoPresupuesto Estado   { get; set; }
    }
}
