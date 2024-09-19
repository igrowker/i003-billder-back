﻿using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Repository.Interfaces
{
    public interface ITrabajoInterfaceRepository
    {
        Task<Trabajo> CrearTrabajoRepository (Trabajo trabajo);
        Task<Trabajo> UpdateTrabajoRepository(Trabajo trabajo);
        Task<Trabajo> GetTrabajoByIDRepository(int id);
    }
}
