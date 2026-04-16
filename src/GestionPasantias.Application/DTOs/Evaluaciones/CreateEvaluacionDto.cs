namespace GestionPasantias.Application.DTOs.Evaluaciones;

public class CreateEvaluacionDto
{
    public int PasantiaId {get; set;}
    public int SupervisorId {get; set;}
    public int Calificacion {get; set;} 
    public string? Comentarios {get; set;}
}