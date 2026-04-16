using System.Reflection.Metadata;
using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IConvenioRepository
{
    Task<IEnumerable<Convenio>> GetAllAsync();
    Task<Convenio?> GetByIdAsync(int id);
    Task<Convenio> AddAsync(Convenio convenio);
    Task<bool> ExistByPasantiaIdAsync(int pasantiaId);
}