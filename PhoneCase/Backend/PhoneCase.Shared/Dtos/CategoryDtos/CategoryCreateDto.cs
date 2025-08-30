using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Shared.Dtos.CategoryDtos;

public class CategoryCreateDto
{
    [Required(ErrorMessage = "Kategori adı zorunludur!")]
    public string Name { get; set; } = string.Empty;
    [Required(ErrorMessage = "Kategori açıklaması zorunludur!")]
    public string Description { get; set; } = string.Empty;
    [Required(ErrorMessage = "Görsel zorunludur!")]
    public IFormFile Image { get; set; } = null!;
    [Required(ErrorMessage = "Kategorinin tipi zorunludur!")]
    public CategoryType? Type { get; set; }
}
