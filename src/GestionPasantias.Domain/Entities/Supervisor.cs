namespace GestionPasantias.Domain.Entities;

public class Supervisor
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int EmpresaId {get; set;}

    // Navigation
    public User User {get; set;} = null!;
    public Empresa Empresa {get; set;} = null!;

    // Relation with Vacante
    public ICollection<Vacante> Vacantes {get; set;} = new List<Vacante>();
}