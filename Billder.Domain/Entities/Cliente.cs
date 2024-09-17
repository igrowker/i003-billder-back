using Billder.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Domain.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public TipoIdentificacion Identificacion { get; set; }
        public string? NroIdentificacion { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Province { get; set; }
        public string? Country { get; set; }
        public int Phone { get; set; }

    }
}
