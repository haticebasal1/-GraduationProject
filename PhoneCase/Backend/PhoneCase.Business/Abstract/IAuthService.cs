using System;
using PhoneCase.Shared.Dtos.AuthDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface IAuthService
{
    Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto);
    Task<ResponseDto<TokenDto>> LoginAsync(LoginDto loginDto);
}
