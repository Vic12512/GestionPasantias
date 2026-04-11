using GestionPasantias.Domain.Entities;
using GestionPasantias.Application.Interfaces;
using GestionPasantias.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace GestionPasantias.Infrastructure.Repositories;

public class PostulacionRepository: IPostulacionRepository
{
    private readonly AppDbContext _context;

    public PostulacionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Postulacion>> GetAllAsync()
    {
        return await _context.Postulaciones.ToListAsync();
    }

    public async Task<Postulacion?> GetByIdAsync(int id)
    {
        return await _context.Postulaciones
            .Include(p => p.Vacante)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Postulacion> AddAsync(Postulacion postulacion)
    {
        await _context.Postulaciones.AddAsync(postulacion);
        await _context.SaveChangesAsync();
        return postulacion;
    }

    public async Task UpdateAsync(Postulacion postulacion)
    {
        _context.Postulaciones.Update(postulacion);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> EstudianteExistAsync (int estudianteId)
    {
        return await _context.Estudiantes.AnyAsync(e => e.Id == estudianteId);
    }
    public async Task<bool> VacanteExistAsync(int vacanteId)
    {
        return await _context.Vacantes.AnyAsync(v => v.Id == vacanteId);
    }
    public async Task<bool> EstadoExistAsync(int estadoId)
    {
        return await _context.EstadosPostulacion.AnyAsync(e => e.Id == estadoId);
    }
    public async Task<bool> PostulacionExistAsync(int estudianteId, int vacanteId)
    {
        return await _context.Postulaciones.AnyAsync(p => 
            p.EstudianteId == estudianteId && p.VacanteId == vacanteId);
    }
}