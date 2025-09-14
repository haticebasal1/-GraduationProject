using System;
using PhoneCase.Shared.Dtos.AuthDtos;

namespace PhoneCase.Shared.Dtos.CartDtos;

public class CartDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public UserDto? User { get; set; }
    public string? ProductName { get; set; }
    public ICollection<CartItemDto> CartItems { get; set; } = [];
    public decimal TotalAmount => CartItems.Sum(x => x.ItemCount);
    public int ItemsCount => CartItems == null ? 0 : CartItems.Count;
}
