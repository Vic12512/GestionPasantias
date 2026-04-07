using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<User>> GetAllAsync();
    Task<User?> GetByIdAsync(int id);
    Task<User> AddAsync(User user);
    Task<User?> GetByEmailAsync(String email);
    Task<bool> EmailExistAsync(string email);
}