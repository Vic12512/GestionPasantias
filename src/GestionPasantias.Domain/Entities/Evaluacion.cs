namespace GestionPasantias.Domain.Entities;

public class Evaluaciones
{
    public int Id {get; set;}
    public int PasantiaId {get; set;}
    public int SupervisorId {get; set;}
    public int Calificacion {get; set;}
    public string? Comentarios {get; set;}
    public DateTime Fecha {get; set;}

    // Navigation
    public Pasantia Pasantia {get; set;} = null!;
    public Supervisor Supervisor {get; set;} = null!;

}