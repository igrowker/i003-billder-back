﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Infrastructure.DTOs
{
    public class ContratoDTO
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int? TrabajoId { get; set; }
        public string? Condiciones { get; set; }
    }
}
