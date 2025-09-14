using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NToastNotify;
using PhoneCase.Shared.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using PhoneCase.Shared.Dtos.ResponseDtos;
using PhoneCase.Shared.Dtos.OrderDtos;

namespace PhoneCase.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class OrderController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IToastNotification _toastr;

    public OrderController(IHttpContextAccessor httpContextAccessor, IToastNotification toastr)
    {
        _httpContextAccessor = httpContextAccessor;
        _toastr = toastr;
    }

    public async Task<IActionResult> Index([FromQuery] OrderStatus? orderstatus, [FromQuery] string? applicationUserId, [FromQuery] DateTime startDate, [FromQuery] DateTime? endDate, [FromQuery] bool? isDeleted)
    {
        ViewBag.OrderStatusList = GetOrderStatuses(orderstatus);
        ViewBag.GetOrderStatusSelectList = new Func<OrderStatus?, List<SelectListItem>>(GetOrderStatuses);

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5289/orders/getall?OrderStatus={orderstatus}l&UserId={applicationUserId}&StartDate={startDate}&EndDate={endDate}&IsDeleted={isDeleted}");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<OrderDto>>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return View(result!.Data);
            }
            return View(result!.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }
    private List<SelectListItem> GetOrderStatuses(OrderStatus? selectedStatus)
    {
        return Enum
            .GetValues(typeof(OrderStatus))
            .Cast<OrderStatus>()
            .Select(x => new SelectListItem
            {
                Text = x
                    .GetType()
                    .GetField(x.ToString())?
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .Cast<DisplayAttribute>()
                    .FirstOrDefault()?
                    .Name ?? x.ToString(),
                Value = ((int)x).ToString(),
                Selected = x == selectedStatus
            }).ToList();
    }
}