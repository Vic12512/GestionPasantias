namespace GestionPasantias.Domain.Entities;

public class EstadoPostulacion
{
    public int Id {get; set;}
    public string Nombre {get; set;} = null!;

    // Navigation
    public ICollection<Postulacion> Postulaciones {get; set;} = new List<Postulacion>();
}