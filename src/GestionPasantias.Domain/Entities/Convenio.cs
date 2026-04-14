namespace GestionPasantias.Domain.Entities;

public class Convenio
{
    public int Id {get; set;}
    public int PasantiaId {get; set;}

    public string Numero {get; set;} = null!;
    public DateTime FechaInicio {get; set;}
    public DateTime? FechaFin {get; set;}
    public DateTime FechaGeneracion {get; set;}

    public string NombreEstudiante {get; set;} = null!;
    public string NombreEmpresa {get; set;} = null!;
    public string NombreUniversidad{get; set;} = null!;

    // Navigation
    public Pasantia Pasantia {get; set;} = null!;
}