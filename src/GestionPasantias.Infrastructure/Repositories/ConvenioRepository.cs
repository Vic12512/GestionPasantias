using Microsoft.EntityFrameworkCore;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using GestionPasantias.Infrastructure.Data;

namespace GestionPasantias.Infrastructure.Repositories;

public class ConvenioRepository : IConvenioRepository
{
    private readonly AppDbContext _context;
    public ConvenioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Convenio>> GetAllAsync()
    {
        return await _context.Convenios.ToListAsync();
    }

    public async Task<Convenio?> GetByIdAsync(int id)
    {
        return await _context.Convenios.FindAsync(id);
    }

    public async Task<Convenio> AddAsync(Convenio convenio)
    {
        await _context.Convenios.AddAsync(convenio);
        await _context.SaveChangesAsync();
        return convenio;
    }

    public async Task<bool> ExistByPasantiaIdAsync(int pasantiaId)
    {
        return await _context.Convenios.AnyAsync(c => c.PasantiaId == pasantiaId);
    }
}