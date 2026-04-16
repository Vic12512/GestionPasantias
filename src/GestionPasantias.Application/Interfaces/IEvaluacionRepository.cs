using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IEvaluacionRepository
{
    Task<IEnumerable<Evaluacion>> GetAllAsync();
    Task<Evaluacion?> GetByIdAsync(int id);
    Task<Evaluacion> AddAsync(Evaluacion evaluacion);

    Task<bool> PasantiaExistAsync(int pasantiaId);
    Task<bool> SupervisorExistAsync(int supervisorId);
    Task<bool> ExistByPasantiaIdAsync(int pasantiaId);
}