using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PhoneCase.Business.Abstract;
using PhoneCase.Entities.Concrete;
using PhoneCase.Shared.Configurations.Auth;
using PhoneCase.Shared.Dtos.AuthDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Concrete;

public class AuthManager : IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtConfig _jwtConfig;

    public AuthManager(UserManager<User> userManager, IOptions<JwtConfig> options)
    {
        _userManager = userManager;
        _jwtConfig = options.Value;
    }

    public async Task<ResponseDto<TokenDto>> LoginAsync(LoginDto loginDto)
    {
        try
        {
            var user =
            await _userManager.FindByNameAsync(loginDto.UserNameOrEmail!)
            ?? await _userManager.FindByEmailAsync(loginDto.UserNameOrEmail!);
            if (user is null)
            {
                return ResponseDto<TokenDto>.Fail("Kullanıcı bulunamadı!", StatusCodes.Status404NotFound);
            }
            var isValidPass = await _userManager.CheckPasswordAsync(user, loginDto.Password!);
            if (!isValidPass)
            {
                return ResponseDto<TokenDto>.Fail("Şifre Hatalı!", StatusCodes.Status400BadRequest);
            }
            var responseDto = await GenerateTokenAsync(user);
            return responseDto;
        }
        catch (Exception ex)
        {
            return ResponseDto<TokenDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    public Task<ResponseDto<UserDto>> RegisterAsync(RegisterDto registerDto)
    {
        throw new NotImplementedException();
    }

    private async Task<ResponseDto<TokenDto>> GenerateTokenAsync(User user)
    {
        try
        {
            var roleList = await _userManager.GetRolesAsync(user);
            var claimList = new List<Claim>
            {
                new (ClaimTypes.NameIdentifier, user.Id),
                new (ClaimTypes.Name, user.UserName!)
            }.Union(roleList.Select(x => new Claim(ClaimTypes.Role, x)));

            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret!));
            var credentials = new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);

            var expiry = DateTime.Now.AddDays(_jwtConfig.AccessTokenExpiration);

            var token = new JwtSecurityToken(
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                claims: claimList,
                expires: expiry,
                signingCredentials: credentials
            );
            var refreshToken = GenerateRefreshAsync();
            var refreshTokenExpiry = DateTime.Now.AddDays(_jwtConfig.RefreshTokenExpiration);

            var tokenDto = new TokenDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                AccessTokenExpretionDate = expiry,
                RefreshToken = refreshToken,
                RefreshTokenExpiration = refreshTokenExpiry
            };
            return ResponseDto<TokenDto>.Success(tokenDto, StatusCodes.Status200OK);
        }
        catch (Exception ex)
        {
            return ResponseDto<TokenDto>.Fail($"Beklenmedik Hata:{ex.Message}", StatusCodes.Status500InternalServerError);
        }
    }

    private string GenerateRefreshAsync()
    {
        var randomBytes = new byte[32];
        using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }
}
