namespace GestionPasantias.Application.DTOs;

public class createUserDto
{
    public string Email {get; set;} = null!;
    public string PasswordHash {get; set;} = null!;    
    public int RolId {get; set;}
}

public class UserDto
{
    public int id {get; set;}
    public string Email {get; set;} = null!;
    public int RolId {get; set;}
}