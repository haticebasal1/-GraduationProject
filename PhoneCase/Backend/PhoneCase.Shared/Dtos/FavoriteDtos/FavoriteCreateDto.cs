using System;
using System.Data;

namespace PhoneCase.Shared.Dtos.FavoriteDtos;

public class FavoriteCreateDto
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}
