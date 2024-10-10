using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class TrabajoClienteDTO
    {
        public TrabajoDTO Trabajo { get; set; } = null!;
        public ClienteDTO Cliente { get; set; } = null!;

    }
}
