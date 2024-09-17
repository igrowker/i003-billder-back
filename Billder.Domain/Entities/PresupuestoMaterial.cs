using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class PresupuestoMaterial
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int MaterialId { get; set; }
        public float Cantidad { get; set; }
        public float CostoMaterial { get; set; }

    }
}
