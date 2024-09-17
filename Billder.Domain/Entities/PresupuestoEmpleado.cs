using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class PresupuestoEmpleado
    {
        public int Id { get; set; }
        public int PresupuestoId { get; set; }
        public int EmpleadoId { get; set; }
        public float HorasTrabajadas { get; set; }
        public float CostoHora { get; set; }


    }
}
