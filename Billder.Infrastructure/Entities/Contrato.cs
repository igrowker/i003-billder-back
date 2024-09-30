using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class Contrato
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int? TrabajoId { get; set; }
        public string? Condiciones { get; set; }

        public virtual Trabajo? Trabajo { get; set; }
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
    }
}
