using Microsoft.EntityFrameworkCore;
using GestionPasantias.Domain.Entities;
using Microsoft.EntityFrameworkCore.Migrations.Operations;


namespace GestionPasantias.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        :base(options)
    {
    }

    // Academico
    public DbSet<Universidad> Universidades => Set<Universidad>();
    public DbSet<Carrera> Carreras => Set<Carrera>();
    public DbSet<Tutor> Tutores => Set<Tutor>();
    public DbSet<Estudiante> Estudiantes => Set<Estudiante>();

    // Empresa
    public DbSet<Empresa> Empresas => Set<Empresa>();
    public DbSet<Supervisor> Supervisores => Set<Supervisor>();

    // Nucleo
    public DbSet<Vacante> Vacantes => Set<Vacante>();
    public DbSet<Postulacion> Postulaciones => Set<Postulacion>();
    public DbSet<EstadoPostulacion> EstadosPostulacion => Set<EstadoPostulacion>();

    // Operaciones
    public DbSet<Pasantia> Pasantias => Set<Pasantia>();
    public DbSet<Evaluacion> Evaluaciones => Set<Evaluacion>();
    public DbSet<Convenio>  Convenios => Set<Convenio>();

    // Chat
    public DbSet<Chat> Chats => Set<Chat>();
    public DbSet<Mensaje> Mensajes => Set<Mensaje>();

    // Usuario
    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        //Configuraciones

        modelBuilder.Entity<Postulacion>()
            .HasIndex(p => new { p.EstudianteId, p.VacanteId})
            .IsUnique();

        modelBuilder.Entity<Pasantia>()
        .HasOne(p => p.Evaluacion)
        .WithOne(e => e.Pasantia)
        .HasForeignKey<Evaluacion>(e => e.PasantiaId);

        modelBuilder.Entity<Pasantia>()
        .HasOne(p => p.Convenio)
        .WithOne(c => c.Pasantia)
        .HasForeignKey<Convenio>(c => c.PasantiaId);

        modelBuilder.Entity<Universidad>()
        .HasOne(u => u.User)
        .WithOne(u => u.Universidad)
        .HasForeignKey<Universidad>(u => u.UserId);

        modelBuilder.Entity<Empresa>()
            .HasOne(e => e.User)
            .WithOne(u => u.Empresa)
            .HasForeignKey<Empresa>(e => e.UserId);

        modelBuilder.Entity<Tutor>()
            .HasOne(t => t.User)
            .WithOne(u => u.Tutor)
            .HasForeignKey<Tutor>(t => t.UserId);

        modelBuilder.Entity<Supervisor>()
            .HasOne(s => s.User)
            .WithOne(u => u.Supervisor)
            .HasForeignKey<Supervisor>(s => s.UserId);

        modelBuilder.Entity<Estudiante>()
            .HasOne(e => e.User)
            .WithOne(u => u.Estudiante)
            .HasForeignKey<Estudiante>(e => e.UserId);
        
        modelBuilder.Entity<ChatParticipante>()
        .HasKey(cp => new { cp.ChatId, cp.UserId });

        modelBuilder.Entity<ChatParticipante>()
            .HasOne(cp => cp.Chat)
            .WithMany(c => c.Participantes)
            .HasForeignKey(cp => cp.ChatId);

        modelBuilder.Entity<ChatParticipante>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.ChatParticipantes)
            .HasForeignKey(cp => cp.UserId);
            }
}