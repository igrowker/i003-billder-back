using Billder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Trabajo
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int ClienteId {  get; set; }
        public int PresupuestoId { get; set; }
        public string? Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
        public EstadoTrabajo Estado { get; set; }
    }
}
