using GestionPasantias.Domain.Entities;
using GestionPasantias.Infrastructure.Data;
using GestionPasantias.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GestionPasantias.Infrastructure.Repositories;

public class PasantiaRepository : IPasantiaRepository
{
    private readonly AppDbContext _context;

    public PasantiaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Pasantia> AddAsync(Pasantia pasantia)
    {
        await _context.Pasantias.AddAsync(pasantia);
        await _context.SaveChangesAsync();
        return pasantia;
    }

    public async Task<bool> ExistByPostulacionIdAsync(int postulacionId)
    {
        return await _context.Pasantias.AnyAsync(p => p.PostulacionId == postulacionId);
    }
}