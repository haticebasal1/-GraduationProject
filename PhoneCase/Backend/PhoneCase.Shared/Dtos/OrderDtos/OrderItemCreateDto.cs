using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using PhoneCase.Shared.Dtos.ProductDtos;
namespace PhoneCase.Shared.Dtos.OrderDtos;

public class OrderItemCreateDto
{
    [Required(ErrorMessage = "Ürün id'si zorunludur!")]
    public int ProductId { get; set; }
    [Required(ErrorMessage = "Ürün fiyatı zorunludur!")]
    public decimal UnitPrice { get; set; }
    [Required(ErrorMessage = "Adet bilgisi zorunludur!")]
    public int Quantity { get; set; }
    public ProductDto? Product { get; set; }
}
