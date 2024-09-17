using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Empleado
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        public string? DNI { get; set; }
        public string? Puesto { get; set; }
    }
}
