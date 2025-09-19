using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.Shared.Dtos.AuthDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        public IActionResult Login(string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl ?? Url.Content("~/");
            return View();
        }

[HttpPost]
public async Task<IActionResult> Login(LoginDto loginDto, string? returnUrl)
{
    returnUrl ??= Url.Content("~/");

    if (!ModelState.IsValid)
    {
        ViewBag.ReturnUrl = returnUrl;
        return View(loginDto);
    }

    using var client = new HttpClient();
    var jsonContent = JsonConvert.SerializeObject(loginDto);
    var stringContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

    var response = await client.PostAsync("http://localhost:5289/auths/login", stringContent);
    var responseContent = await response.Content.ReadAsStringAsync();
    var responseDto = JsonConvert.DeserializeObject<ResponseDto<TokenDto>>(responseContent);

    if (responseDto is not null && responseDto.IsSuccessful)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(responseDto.Data.AccessToken);
        var claims = new List<Claim>(jwtToken.Claims);

        // 🔥 Access token ve refresh token'ı claim olarak ekledim
        claims.Add(new Claim("access_token", responseDto.Data.AccessToken!));
        if (!string.IsNullOrEmpty(responseDto.Data.RefreshToken))
            claims.Add(new Claim("refresh_token", responseDto.Data.RefreshToken));

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

        var authProperties = new AuthenticationProperties
        {
            ExpiresUtc = responseDto.Data.AccessTokenExpretionDate,
            IsPersistent = true, // tarayıcı kapansa da cookie tutulsun istiyorsan
            Items =
            {
                { "access_token", responseDto.Data.AccessToken },
                { "refresh_token", responseDto.Data.RefreshToken ?? "" }
            }
        };

        await _httpContextAccessor.HttpContext!.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimsPrincipal,
            authProperties
        );

        return LocalRedirect(returnUrl); // 🔥 önemli
    }

    // Eğer login başarısızsa
    ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
    ViewBag.ReturnUrl = returnUrl;
    return View(loginDto);
}



        public async Task<IActionResult> Logout()
        {
            await _httpContextAccessor.HttpContext!.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Auth");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
