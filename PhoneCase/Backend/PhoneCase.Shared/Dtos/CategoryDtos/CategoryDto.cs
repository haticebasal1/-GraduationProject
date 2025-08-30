using System;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Shared.Dtos.CategoryDtos;

public class CategoryDto
{
    public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public DateTime DeletedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public CategoryType Type { get; set; }
    public int ProductCount { get; set; }
}
