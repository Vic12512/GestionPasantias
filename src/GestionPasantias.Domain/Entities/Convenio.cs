namespace GestionPasantias.Domain.Entities;

public class Convenio
{
    public int Id {get; set;}
    public int PasantiaId {get; set;}
    public String Numero {get; set;} = null!;
    public DateTime FechaGeneracion {get; set;}
    public String Detalles {get; set;} = null!;

    // Navigation
    public Pasantia Pasantia {get; set;} = null!;
}