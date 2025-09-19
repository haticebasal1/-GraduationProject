using System;
namespace PhoneCase.Shared.Dtos.FavoriteDtos;

public class FavoriteDto
{
    public int Id { get; set; }
    public string? UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
}
