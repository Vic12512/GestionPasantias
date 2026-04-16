using Microsoft.EntityFrameworkCore;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Domain.Entities;
using GestionPasantias.Infrastructure.Data;

namespace GestionPasantias.Infrastructure.Repositories;

public class EvaluacionRepository: IEvaluacionRepository
{
    private readonly AppDbContext _context;

    public EvaluacionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Evaluacion>> GetAllAsync()
    {
        return await _context.Evaluaciones.ToListAsync();
    }
    
    public async Task<Evaluacion?> GetByIdAsync(int id)
    {
        return await _context.Evaluaciones.FindAsync(id);
    }

    public async Task<Evaluacion> AddAsync(Evaluacion evaluacion)
    {
        await _context.Evaluaciones.AddAsync(evaluacion);
        await _context.SaveChangesAsync();
        return evaluacion;
    }
    public async Task<bool> PasantiaExistAsync(int pasantiaId)
    {
        return await _context.Pasantias.AnyAsync(p => p.Id == pasantiaId);
    }
    public async Task<bool> SupervisorExistAsync(int supervisorId)
    {
        return await _context.Supervisores.AnyAsync(s => s.Id == supervisorId);
    }
    public async Task<bool> ExistByPasantiaIdAsync(int pasantiaId)
    {
        return await _context.Evaluaciones.AnyAsync(e => e.PasantiaId == pasantiaId);
    }
}