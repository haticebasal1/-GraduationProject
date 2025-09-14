using System;
using PhoneCase.Shared.Dtos.ProductDtos;

namespace PhoneCase.Shared.Dtos.CartDtos;

public class CartItemDto
{
    public int Id { get; set; }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public ProductDto? Product { get; set; }
    public int Quantity { get; set; }
    public decimal ItemCount => Product!.Price + Quantity;
}
