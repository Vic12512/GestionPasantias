namespace GestionPasantias.Application.DTOs.Auth;

public class RegisterDto
{
    public string Email {get; set;} = null!;
    public string PasswordHash {get; set;} = null!;    
    public int RolId {get; set;}
}