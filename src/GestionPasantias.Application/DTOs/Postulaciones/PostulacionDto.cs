namespace GestionPasantias.Application.DTOs.Postulaciones;

public class PostulacionDto
{
    public int Id {get; set;}
    public int EstudianteId {get; set;}
    public int VacanteId {get; set;}
    public int EstadoPostulacionId {get; set;}
    public DateTime FechaPostulacion {get; set;}
}