using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace PhoneCase.Shared.Dtos.ProductDtos;

public class ProductUpdateDto
{
    [Required(ErrorMessage = "Ürün Id bilgisi zorunludur!")]
    public int Id { get; set; }
    [Required(ErrorMessage = "Ürün adı zorunludur!")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Ürün özellikleri zorunludur!")]
    public string Properties { get; set; } = string.Empty;
    [Required(ErrorMessage = "Ürün fiyatı zorunludur!")]
    public decimal? Price { get; set; }
    public IFormFile? Image { get; set; } = null;
    public bool IsHome { get; set; }
    [Required(ErrorMessage = "En az bir kategori seçilmelidir!")]
    public ICollection<int> CategoryIds { get; set; } = [];
}
