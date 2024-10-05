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
        public async Task<Contrato> GetContratoByID(int id)
        {
            return await _contratoRepository.GetContratoByIDRepository(id);
        }
        public async Task<int> DeleteContrato(int id)
        {
            return await _contratoRepository.DeleteContratoRepository(id);
        }
        public async Task<Contrato> UpdateContrato(Contrato contrato)
        {
            return await _contratoRepository.UpdateContratoRepository(contrato);
        }
        public async Task<List<Contrato>> GetHistorialDeContratos(int usuarioID, int numeroPagina)
        {
            return await _contratoRepository.GetHistorialDeContratosRepository(usuarioID, numeroPagina);
        }
    }
}
