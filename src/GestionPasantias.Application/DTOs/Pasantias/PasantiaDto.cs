namespace GestionPasantias.Application.DTOs.Pasantias;

public class PasantiaDto
{
    public int Id {get; set;}
    public int PostulacionId {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime? FechaFin {get; set;}
}