namespace GestionPasantias.Domain.Entities;

public class Rol
{
    public int Id {get; set;}
    public string Nombre {get; set;} = null!;
    
    //Navigation Property
    public ICollection<User> Users {get; set;} = new List<User>();
}