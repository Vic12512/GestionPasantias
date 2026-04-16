using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IPasantiaRepository
{
    Task<IEnumerable<Pasantia>> GetAllAsync();
    Task<Pasantia?> GetByIdAsync(int id);
    Task<Pasantia> AddAsync(Pasantia pasantia);
    Task<bool> ExistByPostulacionIdAsync(int postulacion);
}