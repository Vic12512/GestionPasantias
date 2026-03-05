using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();

    Task<User?> GetIdAsync(int id);

    Task AddAsync(User user);
}