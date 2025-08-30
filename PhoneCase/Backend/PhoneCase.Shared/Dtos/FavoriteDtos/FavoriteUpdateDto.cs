using System;
using System.ComponentModel.DataAnnotations;
namespace PhoneCase.Shared.Dtos.FavoriteDtos;

public class FavoriteUpdateDto
{
    [Required(ErrorMessage = " Id bilgisi zorunludur!")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Kullanıcı Id bilgisi zorunludur!")]
    public int UserId { get; set; }
    [Required(ErrorMessage = " Ürün Id bilgisi zorunludur!")]
    public int ProductId { get; set; }
}
