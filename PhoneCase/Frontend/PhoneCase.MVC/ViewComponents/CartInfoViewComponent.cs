using System.Net.Http.Headers;
using PhoneCase.Shared.Dtos.CartDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace PhoneCase.MVC.ViewComponents;

public class CartInfoViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IToastNotification _toastr;

    public CartInfoViewComponent(IHttpContextAccessor httpContextAccessor, IToastNotification toastr)
    {
        _httpContextAccessor = httpContextAccessor;
        _toastr = toastr;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            return View("Index", 0);
        }

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5289/carts");

        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<CartDto>>(responseContent);

            var itemsCount = result?.Data?.ItemsCount ?? 0; 

            return View("Index", itemsCount);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            return View("Index", 0);
        }
    }
}
