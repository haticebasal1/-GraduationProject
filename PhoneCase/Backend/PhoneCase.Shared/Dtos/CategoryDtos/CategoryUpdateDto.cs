using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Shared.Dtos.CategoryDtos;

public class CategoryUpdateDto
{
    [Required(ErrorMessage = "Id bilgisi zorunludur!")]
    [Display(Name = "Id")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Kategori adı zorunludur!")]
    [Display(Name = "Kategori")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kategori açıklaması zorunludur!")]
    [Display(Name = "Açıklama")]
    public string Description { get; set; } = string.Empty;
    [Display(Name = "Kategori")]
    public IFormFile? Image { get; set; } = null!;

    [Required(ErrorMessage = "Kategorinin tipi zorunludur!")]
    [Display(Name = "Kategori tipi")]

    public CategoryType Type { get; set; }
}
