using GestionPasantias.Domain.Entities;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionPasantias.Infrastructure.Repositories;

public class VacanteRepository: IVacanteRepository
{
    private readonly AppDbContext _context;

    public VacanteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Vacante>> GetAllAsync()
    {
        return await _context.Vacantes.ToListAsync();
    }

    public async Task<Vacante?> GetByIdAsync(int id)
    {
        return await _context.Vacantes.FindAsync(id);
    }

    public async Task<Vacante> AddAsync (Vacante vacante)
    {
        await _context.AddAsync(vacante);
        await _context.SaveChangesAsync();
        return vacante;
    }

    public async Task<bool> EmpresaExistAsync (int empresaId)
    {
        return await _context.Empresas.AnyAsync(e => e.Id == empresaId);
    }
    public async Task<bool> SupervisorExistAsync(int supervisorId)
    {
        return await _context.Supervisores.AnyAsync(s => s.Id == supervisorId);
    }
}