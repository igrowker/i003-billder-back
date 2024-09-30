﻿using System;
using System.Collections.Generic;

namespace Billder.Infrastructure.Entities
{
    public partial class PresupuestoMaterial
    {
        public int Id { get; set; }
        public int? PresupuestoID { get; set; }
        public int? MaterialID { get; set; }
        public int UsuarioId { get; set; }
        public int? Cantidad { get; set; }
        public decimal? Costo { get; set; }
        public string? Observacion { get; set; }

        public virtual Material? Material { get; set; }
        public virtual Presupuesto? Presupuesto { get; set; }
        public virtual UsuarioRegistrado Usuario { get; set; } = null!;
    }
}
