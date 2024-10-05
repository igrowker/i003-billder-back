
using Billder.Application.Interfaces;
using Billder.Application.Repository.Interfaces;
using Billder.Infrastructure.Entities;

namespace Billder.Application.Services
{   //1
    //Crear el 'TrabajoService' que gestione la lógica relacionada con el registro y consulta de trabajos en la plataforma.
    //El servicio debe manejar correctamente la persistencia y actualización del estado de los trabajos.
    //2
    //Desarrollar el endpoint POST /api/trabajos que permita a los usuarios registrar
    //un nuevo trabajo en la plataforma.Los datos deben incluir el cliente,
    //la descripción del trabajo, la fecha y el estado del proyecto.
    //3
    //Revisar y optimizar las consultas SQL relacionadas con los trabajos en la base de datos, asegurándose de que sean eficientes,
    //especialmente cuando se manejen grandes volúmenes de datos.Mejorar los tiempos de respuesta en la plataforma.
    //4
    //Desarrollar el endpoint GET /api/trabajos que permita obtener el historial completo de trabajos realizados por el usuario.
    //Asegurarse de que los datos sean precisos y que los trabajos se puedan filtrar por fecha y estado.
    public class TrabajoService : ITrabajoInterface
    {
        private readonly ITrabajoRepository _trabajoRepository;

        public TrabajoService(ITrabajoRepository trabajoRepository)
        {
            _trabajoRepository = trabajoRepository;
        }

        public async Task<Trabajo> CrearTrabajo(Trabajo trabajo)
        {
            return await _trabajoRepository.CrearTrabajoRepository(trabajo);

        }

        public async Task<Trabajo> GetTrabajoByID(int id)
        {
            return await _trabajoRepository.GetTrabajoByIDRepository(id);
        }

        public async Task<Trabajo> UpdateTrabajo(Trabajo trabajo)
        {
            return await _trabajoRepository.UpdateTrabajoRepository(trabajo);
        }
        public async Task<int> DeleteTrabajo(int id)
        {
            return await _trabajoRepository.DeleteTrabajoRepository(id);
        }
        public async Task<List<Trabajo>> GetHistorialDeTrabajos(int clienteID, int numeroPagina, string ordenamiento)
        {
            return await _trabajoRepository.GetHistorialDeTrabajosRepository(clienteID,numeroPagina,ordenamiento);
        }
    }
}
