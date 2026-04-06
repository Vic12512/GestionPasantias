namespace GestionPasantias.Domain.Entities;

public class Evaluacion
{
    public int Id {get; set;}
    public int PasantiaId {get; set;}
    public int SupervisorId {get; set;}
    public int Calificacion {get; set;}
    public string? Comentarios {get; set;}
    public DateTime Fecha {get; set;} = DateTime.UtcNow;

    // Navigation
    public Pasantia Pasantia {get; set;} = null!;
    public Supervisor Supervisor {get; set;} = null!;

}