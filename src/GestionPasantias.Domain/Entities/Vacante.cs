namespace GestionPasantias.Domain.Entities;

public class Vacante
{
    public int Id {get; set;}
    public int EmpresaId {get; set;}
    public int SupervisorId {get; set;}
    public string Titulo {get; set;} = null!;
    public string? Descripcion {get; set;} 
    public int Cantidad {get; set;}
    public DateTime FechaInicio {get; set;}
    public DateTime FechaFin {get; set;}

    // Navigation
    public Empresa Empresa {get; set;} = null!;
    public Supervisor Supervisor {get; set;} = null!;
    public ICollection<Postulacion> postulaciones {get; set;} = new List<Postulacion>();
}