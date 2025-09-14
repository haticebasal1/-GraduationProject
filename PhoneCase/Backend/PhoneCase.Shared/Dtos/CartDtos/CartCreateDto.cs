using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneCase.Shared.Dtos.CartDtos;

public class CartCreateDto
{
    [Required(ErrorMessage ="Kullanıcı id bilgisi zorunludur!")]
    public string? UserId { get; set; }
}
