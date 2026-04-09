using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IPostulacionRepository
{
    Task<IEnumerable<Postulacion>> GetAllAsync();
    Task<Postulacion?> GetByIdAsync(int id);
    Task<Postulacion> AddAsync(Postulacion Postulacion);
    Task<bool> EstudianteExistAsync(int estudianteId);
    Task<bool> VacanteExistAsync(int vacanteId);
    Task<bool> PostulacionExistAsync(int estudianteId, int vacanteId);

}