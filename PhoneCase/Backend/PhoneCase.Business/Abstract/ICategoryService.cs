using System;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface ICategoryService
{
    Task<ResponseDto<CategoryDto>> GetAsync(int id);
    Task<ResponseDto<IEnumerable<CategoryDto>>> GetAllAsync(bool? isDeleted = false);
    Task<ResponseDto<int>> CountAsync(bool? isDeleted = false);
    Task<ResponseDto<CategoryDto>> AddAsync(CategoryCreateDto categoryCreateDto);
    Task<ResponseDto<NoContentDto>> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
    Task<ResponseDto<NoContentDto>> HardDeleteAsync(int id);
    Task<ResponseDto<NoContentDto>> SoftDeleteAsync(int id);
}
