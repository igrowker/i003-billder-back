using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Acuerdo
    {
        public int Id { get; set; }
        public int TrabajoId { get; set; }
        public string? Condiciones { get; set; }
        public string? FirmaDigital { get; set; }
    }
}
