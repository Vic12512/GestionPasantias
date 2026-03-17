namespace GestionPasantias.Domain.Entities;

public class Carrera
{
    public int Id {get; set;}
    public int UniversidadId {get; set;}
    public string Nombre {get; set;} = null!;

    // Navigation
    public Universidad  Universidad {get; set;} = null!;
    public ICollection<Estudiante> Estudiantes {get; set;} = new List<Estudiante>();
}