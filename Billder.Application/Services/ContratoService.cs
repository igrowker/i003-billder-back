using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Billder.Application.Services
{
    public class ContratoService : IContratoService
    {
        private readonly IContratoRepository _contratoRepository;

        public ContratoService(IContratoRepository contratoRepository)
        {
            _contratoRepository = contratoRepository;
        }


        public async Task<Contrato> CrearContrato(Contrato contrato)
        {
            return await _contratoRepository.CrearContratoRepository(contrato);
        }
        public async Task<Contrato> GetContratoByID(int id, int userId)
        {
            return await _contratoRepository.GetContratoByIDRepository(id, userId);
        }
        public async Task<int> DeleteContrato(int id, int userId)
        {
            return await _contratoRepository.DeleteContratoRepository(id, userId);
        }
        public async Task<Contrato> UpdateContrato(Contrato contrato, int userId)
        {
            return await _contratoRepository.UpdateContratoRepository(contrato, userId);
        }
        public async Task<List<Contrato>> GetHistorialDeContratos(int usuarioID, int numeroPagina)
        {
            return await _contratoRepository.GetHistorialDeContratosRepository(usuarioID, numeroPagina);
        }
    }
}
