using Microsoft.EntityFrameworkCore;
using GestionPasantias.Domain.Entities;


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
    public DbSet<Rol> Roles => Set<Rol>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // =========================
        // USER - ROL
        // =========================
        modelBuilder.Entity<User>()
            .HasOne(u => u.Rol)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RolId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // UNIVERSIDAD
        // =========================
        modelBuilder.Entity<Universidad>()
            .HasOne(u => u.User)
            .WithOne(u => u.Universidad)
            .HasForeignKey<Universidad>(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // CARRERA
        // =========================
        modelBuilder.Entity<Carrera>()
            .HasOne(c => c.Universidad)
            .WithMany(u => u.Carreras)
            .HasForeignKey(c => c.UniversidadId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // TUTOR
        // =========================
        modelBuilder.Entity<Tutor>()
            .HasOne(t => t.User)
            .WithOne(u => u.Tutor)
            .HasForeignKey<Tutor>(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Tutor>()
            .HasOne(t => t.Universidad)
            .WithMany(u => u.Tutores)
            .HasForeignKey(t => t.UniversidadId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // ESTUDIANTE
        // =========================
        modelBuilder.Entity<Estudiante>()
            .HasOne(e => e.User)
            .WithOne(u => u.Estudiante)
            .HasForeignKey<Estudiante>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Estudiante>()
            .HasOne(e => e.Carrera)
            .WithMany(c => c.Estudiantes)
            .HasForeignKey(e => e.CarreraId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // EMPRESA
        // =========================
        modelBuilder.Entity<Empresa>()
            .HasOne(e => e.User)
            .WithOne(u => u.Empresa)
            .HasForeignKey<Empresa>(e => e.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // SUPERVISOR
        // =========================
        modelBuilder.Entity<Supervisor>()
            .HasOne(s => s.User)
            .WithOne(u => u.Supervisor)
            .HasForeignKey<Supervisor>(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Supervisor>()
            .HasOne(s => s.Empresa)
            .WithMany(e => e.Supervisores)
            .HasForeignKey(s => s.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // VACANTE
        // =========================
        modelBuilder.Entity<Vacante>()
            .HasOne(v => v.Empresa)
            .WithMany(e => e.Vacantes)
            .HasForeignKey(v => v.EmpresaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Vacante>()
            .HasOne(v => v.Supervisor)
            .WithMany(s => s.Vacantes)
            .HasForeignKey(v => v.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // POSTULACION
        // =========================
        modelBuilder.Entity<Postulacion>()
            .HasIndex(p => new { p.EstudianteId, p.VacanteId })
            .IsUnique();

        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.Estudiante)
            .WithMany(e => e.Postulaciones)
            .HasForeignKey(p => p.EstudianteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.Vacante)
            .WithMany(v => v.Postulaciones)
            .HasForeignKey(p => p.VacanteId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Postulacion>()
            .HasOne(p => p.EstadoPostulacion)
            .WithMany(e => e.Postulaciones)
            .HasForeignKey(p => p.EstadoPostulacionId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // PASANTIA (1:1)
        // =========================
        modelBuilder.Entity<Pasantia>()
            .HasOne(p => p.Postulacion)
            .WithOne(p => p.Pasantia)
            .HasForeignKey<Pasantia>(p => p.PostulacionId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // EVALUACION (1:1)
        // =========================
        modelBuilder.Entity<Evaluacion>()
            .HasOne(e => e.Pasantia)
            .WithOne(p => p.Evaluacion)
            .HasForeignKey<Evaluacion>(e => e.PasantiaId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Evaluacion>()
            .HasOne(e => e.Supervisor)
            .WithMany()
            .HasForeignKey(e => e.SupervisorId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // CONVENIO (1:1)
        // =========================
        modelBuilder.Entity<Convenio>()
            .HasOne(c => c.Pasantia)
            .WithOne(p => p.Convenio)
            .HasForeignKey<Convenio>(c => c.PasantiaId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // CHAT
        // =========================
        modelBuilder.Entity<Mensaje>()
            .HasOne(m => m.Chat)
            .WithMany(c => c.Mensajes)
            .HasForeignKey(m => m.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Mensaje>()
            .HasOne(m => m.User)
            .WithMany(u => u.MensajesEnviados)
            .HasForeignKey(m => m.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ChatParticipante>()
            .HasKey(cp => new { cp.ChatId, cp.UserId });

        modelBuilder.Entity<ChatParticipante>()
            .HasOne(cp => cp.Chat)
            .WithMany(c => c.Participantes)
            .HasForeignKey(cp => cp.ChatId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ChatParticipante>()
            .HasOne(cp => cp.User)
            .WithMany(u => u.ChatParticipantes)
            .HasForeignKey(cp => cp.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // =========================
        // DATA
        // =========================
        modelBuilder.Entity<Rol>().HasData(
            //new Rol { Id = 1, Nombre = "Admin" },
            new Rol { Id = 2, Nombre = "Estudiante" },
            new Rol { Id = 3, Nombre = "Tutor" },
            new Rol { Id = 4, Nombre = "Supervisor" },
            new Rol { Id = 5, Nombre = "Empresa" },
            new Rol { Id = 6, Nombre = "Universidad" }
        );
    }
}