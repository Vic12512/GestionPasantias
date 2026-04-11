using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Application.Interfaces;

public interface IPasantiaRepository
{
    Task<Pasantia> AddAsync(Pasantia pasantia);
}