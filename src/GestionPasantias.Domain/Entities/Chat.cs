namespace GestionPasantias.Domain.Entities;

public class Chat
{
    public int Id {get; set;}
    public DateTime CreadoEn {get; set;}

    // Navigation
    public ICollection<Mensaje> Mensajes {get; set;} = new List<Mensaje>();
}