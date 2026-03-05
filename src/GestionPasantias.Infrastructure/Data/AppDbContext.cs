using Microsoft.EntityFrameworkCore;
using GestionPasantias.Domain.Entities;
using Microsoft.Identity.Client;

namespace GestionPasantias.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<User> Users => Set<User>();
}