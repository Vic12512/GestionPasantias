namespace GestionPasantias.Domain.Entities;

public class Estudiante
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int CarreraId {get; set;}
    public string Martricula {get; set;} = null!;
    
    // Navigation
    public User User {get; set;} = null!;
    public Carrera Carrera {get; set;} = null!;

    // Relation with Postulaciones
    public ICollection<Postulacion> Postulaciones {get; set;} = new List<Postulacion>();
}