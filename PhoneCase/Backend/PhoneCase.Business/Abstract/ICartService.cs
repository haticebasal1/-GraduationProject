using System;
using PhoneCase.Shared.Dtos.CartDtos;
using PhoneCase.Shared.Dtos.CartDtos.ChangeQuantityDto;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface ICartService
{
    Task<ResponseDto<CartDto>> CreateCartAsync(CartCreateDto cartCreateDto);
    Task<ResponseDto<CartDto>> GetCartAsync(string userId);
    Task<ResponseDto<CartItemDto>> AddToCartAsync(AddToCartDto addToCartDto);
    Task<ResponseDto<NoContentDto>> RemoveFromCartAsync(int cartItemId);
    Task<ResponseDto<NoContentDto>> ClearCartAsync(string userId);
    Task<ResponseDto<NoContentDto>> ChangeQuantityAsync(ChangeQuantityDto changeQuantityDto);
}
