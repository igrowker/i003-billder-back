using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record class PresupuestoMaterialDTO(
        int Id, 
        int PresupuestoID, 
        int MaterialID, 
        int Cantidad, 
        decimal Costo, 
        string Observacion 
        );
    
}
