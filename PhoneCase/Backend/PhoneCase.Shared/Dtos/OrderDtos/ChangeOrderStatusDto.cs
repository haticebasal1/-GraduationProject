using System;
using System.ComponentModel.DataAnnotations;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Shared.Dtos.OrderDtos;

public class ChangeOrderStatusDto
{
    [Required(ErrorMessage = "Sipariş id'si zorunludur!")]
    public int OrderId { get; set; }
    [Required(ErrorMessage = "Yeni sipariş durumu bilgisi zorunludur!")]
    public OrderStatus OrderStatus { get; set; }
}
