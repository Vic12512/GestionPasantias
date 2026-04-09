using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IVacanteRepository
{
    Task<IEnumerable<Vacante>> GetAllAsync();
    Task<Vacante?> GetByIdAsync(int id);
    Task<Vacante> AddAsync(Vacante vacante);
    Task<bool> EmpresaExistAsync(int empresa);
    Task<bool> SupervisorExistAsync(int supevisor);
}