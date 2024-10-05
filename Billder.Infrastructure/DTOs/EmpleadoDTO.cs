using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public record class EmpleadoDTO(
        int Id,
        string Fullname, 
        string Identificacion, 
        string NroIdentificacion, 
        string Puesto
        );
    
}
