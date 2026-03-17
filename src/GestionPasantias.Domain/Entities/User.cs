namespace GestionPasantias.Domain.Entities;

public class User
{
    public int Id {get; set; }
    public string Email {get; set; } = null!;
    public string PasswordHash {get; set; } = null!;
    public int RolId {get; set; }

    // Navigation property
    public Rol Rol {get; set;} = null!;

    // Navigation to profiles 
    public Estudiante? Estudiante {get; set;}
    public Tutor? Tutor {get; set;}
    public Supervisor? Supervisor {get; set;}
    public Empresa? Empresa {get; set;}
    public Universidad? Universidad {get; set;}

    // Chat

    public ICollection<ChatParticipante> ChatParticipantes {get; set;} = new List<ChatParticipante>();
    public ICollection<Mensaje> MensajesEnviados {get; set;} = new List<Mensaje>(); 
}