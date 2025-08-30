using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneCase.Shared.Dtos.AuthDtos;

public class RegisterDto
{
    [Required(ErrorMessage = "Ad zorunludur!")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Soyad zorunludur!")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Kullanıcı adı zorunludur!")]
    public string? UserName { get; set; }

    [Required(ErrorMessage = "Mail zorunludur!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Parola zorunludur'")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Parola tekrarı zorunludur'")]
    [Compare("Password",ErrorMessage ="Parolalar eşleşmiyor!")]
    public string? ConfirmPassword { get; set; }
}
