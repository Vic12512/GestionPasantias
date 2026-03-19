namespace GestionPasantias.Domain.Entities;

public class Pasantia
{
    public int Id {get; set;}
    public int PostulacionId {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime? FechaFin {get; set;}

    // Navigation 
    public Postulacion Postulacion {get; set;} = null!;
    public Evaluaciones? Evaluacion {get; set;}
    public Convenio? Convenio {get; set;}

}