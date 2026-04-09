namespace GestionPasantias.Application.DTOs.Vacantes;

public class CreateVacanteDto
{
    public int EmpresaId {get; set;}
    public int SupervisorId {get; set;}
    public string Titulo {get; set;} = null!;
    public string? Descripcion {get; set;}
    public int Cantidad {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime FechaFin {get; set;}
}