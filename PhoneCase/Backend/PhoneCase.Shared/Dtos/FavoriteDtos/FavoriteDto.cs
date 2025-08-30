using System;
namespace PhoneCase.Shared.Dtos.FavoriteDtos;

public class FavoriteDto
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime UpdatedDate { get; set; }
}
