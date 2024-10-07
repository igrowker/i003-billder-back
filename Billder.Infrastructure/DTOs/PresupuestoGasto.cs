using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class PresupuestoGasto
    {
        public PresupuestoDTO Presupuesto { get; set; } = null!;
        public GastoDTO Gasto { get; set; } = null!;
    }
}
