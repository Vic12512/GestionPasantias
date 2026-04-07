using System.Globalization;

namespace GestionPasantias.Application.DTOs.Auth;

public class LoginDto
{
    public string Email {get; set;} = null!;
    public string PasswordHash {get; set;} = null!;
}