using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PhoneCase.Shared.Dtos.OrderDtos;

public class OrderNowDto
{
    [JsonIgnore]
    public string? UserId { get; set; }
    [Required(ErrorMessage = "Siparişe ait ürün bilgisi zorunludur!")]
    public ICollection<OrderItemCreateDto> OrderItems { get; set; } = [];
    [Required(ErrorMessage = "Adres zorunludur!")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Şehir zorunludur!")]
    public string? City { get; set; }
}
