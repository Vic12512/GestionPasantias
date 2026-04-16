namespace GestionPasantias.Application.DTOs.Evaluaciones;

public class EvaluacionDto
{
    public int Id {get; set;}
    public int PasantiaId {get; set;}
    public int SupervisorId {get; set;}
    public int Calificacion {get; set;} 
    public string? Comentarios {get; set;}
    public DateTime Fecha {get; set;}
}