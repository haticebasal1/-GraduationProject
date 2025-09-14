using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneCase.Shared.Dtos.CartDtos;

public class AddToCartDto
{
    [JsonIgnore]
    public string? UserId { get; set; }
    [Required(ErrorMessage ="Ürün id bilgisi zorunludur!")]
    public int ProductId { get; set; }
     [Required(ErrorMessage ="Ürün miktarı zorunludur!")]
    public int Quantity { get; set; }

}
