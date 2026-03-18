namespace GestionPasantias.Domain.Entities;

public class Postulacion
{
    public int Id {get; set;}
    public int EstudianteId {get; set;}
    public int VacanteId {get; set;}
    public int EstadoPostulacionId {get; set;}
    public DateTime FechaPostulacion {get; set;}

    // Navigation 
    public Estudiante Estudiante {get; set;} = null!;
    public Vacante Vacante {get; set;} = null!;
    public EstadoPostulacion EstadoPostulacion {get; set;} = null!;

    public Pasantia? Pasantia {get; set;}
}