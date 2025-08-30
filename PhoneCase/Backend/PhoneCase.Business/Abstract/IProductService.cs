using Azure;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface IProductService
{
    Task<ResponseDto<ProductDto>> GetAsync(int id, bool includeCategories = false);
    Task<ResponseDto<IEnumerable<ProductDto>>> GetAllAsync(
         bool includeCategories = false,
         int? categoryId = null
    );
    Task<ResponseDto<IEnumerable<ProductDto>>> GetAllDeletedAsync();
    Task<ResponseDto<IEnumerable<ProductDto>>> GetAllHomePageAsync();

    Task<ResponseDto<int>> CountAsync(bool? isDeleted = null, int? categoryId = null);
    Task<ResponseDto<ProductDto>> AddAsync(ProductCreateDto productCreateDto);
    Task<ResponseDto<NoContentDto>> UpdateAsync(ProductUpdateDto productUpdateDto);
    Task<ResponseDto<NoContentDto>> HardDeletedAsync(int id);
    Task<ResponseDto<NoContentDto>> SoftDeletedAsync(int id);
    Task<ResponseDto<NoContentDto>> UpdateIsHomeAsync(int id);
    Task<ResponseDto<NoContentDto>> SoftDeletedByCategoryIdAsync(int categoryId);
}
