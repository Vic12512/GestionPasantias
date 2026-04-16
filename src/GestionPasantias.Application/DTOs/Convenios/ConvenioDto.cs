namespace GestionPasantias.Application.DTOs.Convenios;

public class ConvenioDto
{
    public int Id {get; set;} 
    public int PasantiaId {get; set;} 

    public string Numero {get; set;} = null!;

    public DateTime FechaGeneracion {get; set;} 
    public DateTime FechaInicio {get; set;}
    public DateTime? FechaFin {get; set;} 

    public string NombreEstudiante {get; set;} = null!;
    public string NombreEmpresa {get; set;} = null!;
    public string NombreUniversidad {get; set;} = null!;
}