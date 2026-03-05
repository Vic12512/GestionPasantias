using Microsoft.EntityFrameworkCore;
using GestionPasantias.Domain.Entities;

namespace GestionPasantias.Infrastructure.Data;

public class AppDbContext ; DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();
}