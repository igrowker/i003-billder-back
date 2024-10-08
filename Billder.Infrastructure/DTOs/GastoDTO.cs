using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class GastoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PresupuestoId { get; set; }
        public string? Nombre { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Precio { get; set; }
        public decimal? CostoHoraLaboral { get; set; }
        public decimal? HorasTrabajadas { get; set; }
    }
}
