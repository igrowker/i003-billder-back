using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record ContratoDTO
    {
        public int UsuarioId { get; set; }
        public int? TrabajoId { get; set; }
        public string? Condiciones { get; set; }
    }
}
