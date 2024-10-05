using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record class PresupuestoEmpleadoDTO(
        int Id, 
        int PresupuestoId, 
        int EmpleadoId,
        decimal HorasTrabajadas, 
        decimal CostoHora 
        );
    
}
