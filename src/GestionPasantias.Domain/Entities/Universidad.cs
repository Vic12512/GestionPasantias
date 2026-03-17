namespace GestionPasantias.Domain.Entities;

public class Universidad
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string Nombre {get; set;} = null!;
    public string? Direccion {get; set;}

    // Navigation
    public User User {get; set;} = null!;

    public ICollection<Carrera> carreras {get; set;} = new List<Carrera>();
    public ICollection<Tutor> Tutores {get; set;} = new List<Tutor>();
}