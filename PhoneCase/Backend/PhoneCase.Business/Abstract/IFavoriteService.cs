using System;
using PhoneCase.Shared.Dtos.FavoriteDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface IFavoriteService
{
    Task<ResponseDto<FavoriteDto>> AddAsync(FavoriteCreateDto favoriteCreateDto);
    Task<ResponseDto<FavoriteDto>> UpdateAsync(FavoriteUpdateDto favoriteUpdateDto);
    Task<ResponseDto<NoContentDto>> DeleteAsync(int id);
    Task<ResponseDto<FavoriteDto>> GetByIdAsync(int id);
    Task<ResponseDto<IEnumerable<FavoriteDto>>> GetAllAsync(
        int userId= 0,
        int productId = 0,
        bool includeUser=false,
        bool includeProduct = false,
        bool? isDeleted=null
    );
    Task<ResponseDto<NoContentDto>> SoftDeletedAsync(int id);
}
