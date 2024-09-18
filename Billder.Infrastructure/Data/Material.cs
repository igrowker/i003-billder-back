using System;
using System.Collections.Generic;

namespace Billder.Data
{
    public partial class Material
    {
        public Material()
        {
            PresupuestoMaterials = new HashSet<PresupuestoMaterial>();
        }

        public int Id { get; set; }
        public string? Descripcion { get; set; }

        public virtual ICollection<PresupuestoMaterial> PresupuestoMaterials { get; set; }
    }
}
