namespace GestionPasantias.Domain.Entities;

public class ChatParticipante
{
    public int ChatId { get; set; }

    public int UserId { get; set; }


    // 🔗 Navigation
    public Chat Chat { get; set; } = null!;

    public User User { get; set; } = null!;
}