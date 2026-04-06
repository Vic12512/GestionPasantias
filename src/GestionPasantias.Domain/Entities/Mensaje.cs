namespace GestionPasantias.Domain.Entities;

public class Mensaje
{
    public int Id {get; set;}
    public int ChatId {get; set;}
    public int UserId {get; set;}
    public string Contenido {get; set;} = null!;
    public DateTime FechaEnvio {get; set;} = DateTime.UtcNow;

    // Navigation
    public Chat Chat {get; set;} = null!;
    public User User {get; set;} = null!;
}