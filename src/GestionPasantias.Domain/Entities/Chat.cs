using System.Dynamic;

namespace GestionPasantias.Domain.Entities;

public class Chat
{
    public int Id {get; set;}
    public DateTime CreadoEn {get; set;} = DateTime.UtcNow;

    // Navigation
    public ICollection<Mensaje> Mensajes {get; set;} = new List<Mensaje>();
    public ICollection<ChatParticipante> Participantes {get; set;} = new List<ChatParticipante>();
}