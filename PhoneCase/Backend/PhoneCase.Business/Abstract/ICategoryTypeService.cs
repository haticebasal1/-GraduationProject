using System;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface ICategoryTypeService
{
    Task<ResponseDto<IEnumerable<CategoryTypeDto>>> GetAllTypeAsync();
    Task<ResponseDto<CategoryTypeDto>> GetByValueAsync(int value);
}
