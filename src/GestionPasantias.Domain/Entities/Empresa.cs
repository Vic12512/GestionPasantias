namespace GestionPasantias.Domain.Entities;

public class Empresa
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public string Nombre {get; set;} = null!;
    public string? Direccion {get; set;}

    // Navigation
    public User User {get; set;} = null!;
    public ICollection<Supervisor> Supervisores {get; set;} = new List<Supervisor>();
    public ICollection<Vacante> Vacantes {get; set;} = new List<Vacante>();
}