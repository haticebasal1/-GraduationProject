using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Business.Concrete;

public class CategoryTypeManager : ICategoryTypeService
{
    private readonly List<CategoryTypeDto> _categoryTypes;

    public CategoryTypeManager()
    {
        _categoryTypes = Enum.GetValues(typeof(CategoryType))
                 .Cast<CategoryType>()
                 .Select(e => new CategoryTypeDto
                 {
                     Value = (int)e,
                     Name = e.GetType()
                             .GetMember(e.ToString())
                             .First()
                             .GetCustomAttribute<DisplayAttribute>()?.Name ?? e.ToString()
                 }).ToList();

    }

    public Task<ResponseDto<IEnumerable<CategoryTypeDto>>> GetAllTypeAsync()
    {
        try
        {
            return Task.FromResult(ResponseDto<IEnumerable<CategoryTypeDto>>.Success(_categoryTypes, StatusCodes.Status200OK));

        }
        catch (Exception ex)
        {
            return Task.FromResult(ResponseDto<IEnumerable<CategoryTypeDto>>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError));
        }
    }

    public  Task<ResponseDto<CategoryTypeDto>> GetByValueAsync(int value)
    {
        try
        {
            var dto = _categoryTypes.FirstOrDefault(t => t.Value == value);
            if (dto == null)
            {
                return Task.FromResult(ResponseDto<CategoryTypeDto>.Fail($"Geçersiz kategori tipi!", StatusCodes.Status400BadRequest));
            }
            return Task.FromResult(ResponseDto<CategoryTypeDto>.Success(dto, StatusCodes.Status200OK));
        }
        catch (Exception ex)
        {
            return Task.FromResult(ResponseDto<CategoryTypeDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError));
        }
    }
}
