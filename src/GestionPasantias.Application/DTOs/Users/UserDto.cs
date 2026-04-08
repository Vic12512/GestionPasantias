namespace GestionPasantias.Application.DTOs.Users;
public class UserDto
{
    public int Id {get; set;}
    public string Email {get; set;} = null!;
    public int RolId {get; set;}
}