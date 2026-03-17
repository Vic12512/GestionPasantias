namespace GestionPasantias.Domain.Entities;

public class Tutor
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int UniversidadId {get; set;}

    //Navigation 
    public User User {get; set;} = null!;
    public Universidad Universidad {get; set;} = null!;

}