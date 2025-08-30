using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneCase.Shared.Dtos.AuthDtos;

public class LoginDto
{
    [Required(ErrorMessage = "Kullanıcı adı/Mail zorunludur!")]
    public string? UserNameOrEmail { get; set; }

    [Required(ErrorMessage = "Parola boş bırakılamaz!")]
    public string? Password { get; set; }
}
