using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.AuthDtos;

namespace PhoneCase.API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : CustomControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var response = await _authService.LoginAsync(loginDto);
            return CreateResult(response);
        }
    }
}
