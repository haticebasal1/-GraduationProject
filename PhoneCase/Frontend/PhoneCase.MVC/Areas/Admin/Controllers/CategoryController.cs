using System.Net.Http.Headers;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;
using PhoneCase.Shared.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Reflection;
using System.ComponentModel.DataAnnotations;

namespace ECommerce.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class CategoryController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IToastNotification _toastr;

    public CategoryController(IHttpContextAccessor httpContextAccessor, IToastNotification toastr)
    {
        _httpContextAccessor = httpContextAccessor;
        _toastr = toastr;
    }
    public async Task<IActionResult> Index([FromQuery] bool isDeleted = false)
    {
        var client = new HttpClient();
        var url = isDeleted ? $"http://localhost:5289/categories/getall/deleted" : $"http://localhost:5289/categories/getall";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<CategoryDto>>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return View(result!.Data);
            }
            ViewBag.ShowDeleted = isDeleted;
            return View(result!.Data);

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }

[HttpGet]
public IActionResult Create()
{
    ViewBag.CategoryTypes = Enum.GetValues(typeof(CategoryType))
        .Cast<CategoryType>()
        .Select(c => new SelectListItem
        {
            Value = ((int)c).ToString(),
            Text = c.GetType()
                    .GetMember(c.ToString())
                    .First()
                    .GetCustomAttribute<DisplayAttribute>()?.Name ?? c.ToString()
        }).ToList();

    return View();
}


    [HttpPost]
public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
{
    if (!ModelState.IsValid)
    {
        // 🔹 Dropdown'u tekrar dolduruyoruz
        ViewBag.CategoryTypes = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.GetType()
                        .GetMember(c.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? c.ToString(),
                Selected = c == categoryCreateDto.Type
            }).ToList();

        return View(categoryCreateDto);
    }

    var client = new HttpClient();
    try
    {
        var authResult = await _httpContextAccessor.HttpContext!
            .AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        var token = authResult.Properties?.Items["access_token"];
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var formData = new MultipartFormDataContent();
        formData.Add(new StringContent(categoryCreateDto.Name), "Name");
        formData.Add(new StringContent(categoryCreateDto.Description), "Description");
        formData.Add(new StringContent(((int)categoryCreateDto.Type!).ToString()), "Type");

        if (categoryCreateDto.Image is not null)
        {
            formData.Add(new StreamContent(categoryCreateDto.Image.OpenReadStream()), "Image", categoryCreateDto.Image.FileName);
        }
        else
        {
            var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "no-picture.png");
            formData.Add(new StreamContent(System.IO.File.OpenRead(defaultImagePath)), "Image", "no-picture.png");
        }

        var response = await client.PostAsync("http://localhost:5289/categories", formData);
        var responseContent = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<ResponseDto<CategoryDto>>(responseContent);

        if (!result!.IsSuccessful)
        {
            _toastr.AddErrorToastMessage(result!.Errors[0]);
            ViewBag.CategoryTypes = Enum.GetValues(typeof(CategoryType))
                .Cast<CategoryType>()
                .Select(c => new SelectListItem
                {
                    Value = ((int)c).ToString(),
                    Text = c.GetType()
                            .GetMember(c.ToString())
                            .First()
                            .GetCustomAttribute<DisplayAttribute>()?.Name ?? c.ToString(),
                    Selected = c == categoryCreateDto.Type
                }).ToList();

            return View(categoryCreateDto);
        }

        _toastr.AddSuccessToastMessage("İşlem başarılı!");
        return RedirectToAction(nameof(Index));
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Hata:{ex.Message}");
        throw;
    }
}


    public async Task<IActionResult> Edit(int id)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5289/categories/{id}");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<CategoryDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OldImageUrl = result!.Data.ImageUrl;
            var categoryUpdateDto = new CategoryUpdateDto
            {
                Id = result!.Data.Id,
                Name = result!.Data.Name!,
                Description = result!.Data.Description!,
                Type = result!.Data.Type
            };
        ViewBag.CategoryTypes = Enum.GetValues(typeof(CategoryType))
            .Cast<CategoryType>()
            .Select(c => new SelectListItem
            {
                Value = ((int)c).ToString(),
                Text = c.GetType()
                        .GetMember(c.ToString())
                        .First()
                        .GetCustomAttribute<DisplayAttribute>()?.Name ?? c.ToString(),
                Selected = c == categoryUpdateDto.Type
            }).ToList();
        return View(categoryUpdateDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }
    [HttpPost]
    public async Task<IActionResult> Edit(CategoryUpdateDto categoryUpdateDto, string OldImageUrl)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.OldImageUrl = OldImageUrl;
            return View(categoryUpdateDto);
        }
        var client = new HttpClient();
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(categoryUpdateDto.Id.ToString()), "Id");
            formData.Add(new StringContent(categoryUpdateDto.Name), "Name");
            formData.Add(new StringContent(categoryUpdateDto.Description), "Description");
            if (categoryUpdateDto.Image is not null)
            {
                formData.Add(new StreamContent(categoryUpdateDto.Image.OpenReadStream()), "Image", categoryUpdateDto.Image.FileName);
            }
            var response = await client.PutAsync("http://localhost:5289/categories", formData);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<NoContentDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Index));
            }
            _toastr.AddSuccessToastMessage("Güncelleme başarılı");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }
    public async Task<IActionResult> Trash(int id)
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5289/categories/{id}");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<CategoryDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OldImageUrl = result!.Data.ImageUrl;
            return View(result!.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Trash(int id, [FromQuery] bool isSoft = true)
    {
        var client = new HttpClient();
        var isSoftUri = isSoft ? "soft" : "hard";
        var request = new HttpRequestMessage(isSoft ? HttpMethod.Put : HttpMethod.Delete, $"http://localhost:5289/categories/delete/{isSoftUri}?id={id}");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                _toastr.AddErrorToastMessage("Token bilgisi bulunamadığı için işlem yapılamadı!");
                return RedirectToAction(nameof(Index));
            }
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<NoContentDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Trash), new { id });
            }
            _toastr.AddSuccessToastMessage("İşlem başarıyla tamamlanmıştır!");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata:{ex.Message}");
            throw;
        }
    }
}