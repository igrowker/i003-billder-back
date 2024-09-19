using System;
using System.Collections.Generic;

namespace Billder.Domain.Entities
{
    public partial class Contrato
    {
        public int Id { get; set; }
        public int? TrabajoId { get; set; }
        public string? Condiciones { get; set; }

        public virtual Trabajo? Trabajo { get; set; }
    }
}
