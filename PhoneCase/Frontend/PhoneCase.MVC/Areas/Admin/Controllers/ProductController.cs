using System.Net.Http.Headers;
using PhoneCase.Shared.Dtos.CategoryDtos;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NToastNotify;

namespace PhoneCase.MVC.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ProductController : Controller
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IToastNotification _toastr;

    public ProductController(IHttpContextAccessor httpContextAccessor, IToastNotification toastr)
    {
        _httpContextAccessor = httpContextAccessor;
        _toastr = toastr;
    }

    public async Task<IActionResult> Index([FromQuery] bool isDeleted = false)
    {
        var client = new HttpClient();
        var url = isDeleted ? "http://localhost:5289/products/deleted" : "http://localhost:5289/products";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content!.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<ProductDto>>>(responseContent);
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
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> Create()
    {
        var categories = await GetCategoriesAsync();
        if (categories is null)
        {
            _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
            return RedirectToAction(nameof(Index));
        }
        ViewBag.CategoryList = categories;
        return View(new ProductCreateDto());
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
    {
        if (!ModelState.IsValid)
        {
            var categories = await GetCategoriesAsync();
            if (categories is null)
            {
                _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryList = categories;
            if (productCreateDto.CategoryIds is null)
            {
                ModelState.AddModelError("CategoryIds", "En az bir kategori seçilmelidir!");
            }
            return View(productCreateDto);
        }

        var client = new HttpClient();
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(productCreateDto.Name), "Name");
            formData.Add(new StringContent(productCreateDto.Properties), "Properties");
            formData.Add(new StringContent(productCreateDto.Price.ToString()!), "Price");
            formData.Add(new StringContent(productCreateDto.IsHome.ToString()!), "IsHome");
            if (productCreateDto.CategoryIds.Count == 0)
            {
                ModelState.AddModelError("CategoryIds", "En az bir kategori seçilmelidir!");
                var categories = await GetCategoriesAsync();
                if (categories is null)
                {
                    _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.CategoryList = categories;
                return View(productCreateDto);
            }
            foreach (int id in productCreateDto.CategoryIds) // 5,7,8,11
            {
                formData.Add(new StringContent(id.ToString()), "CategoryIds");
            }
            if (productCreateDto.Image is not null)
            {
                formData.Add(new StreamContent(productCreateDto.Image.OpenReadStream()), "Image", productCreateDto.Image.FileName);
            }
            else
            {
                var defaultImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "no-picture.png");
                formData.Add(new StreamContent(System.IO.File.OpenRead(defaultImagePath)), "Image", "no-picture.png");
            }

            var response = await client.PostAsync("http://localhost:5289/products", formData);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<ProductDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return View(productCreateDto);
            }
            _toastr.AddSuccessToastMessage("İşlem başarılı!");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> Edit(int id)
    {
        var categories = await GetCategoriesAsync();
        if (categories is null)
        {
            _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
            return RedirectToAction(nameof(Index));
        }
        ViewBag.CategoryList = categories;

        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5289/products/{id}?includeCategories=true");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<ProductDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.OldImageUrl = result!.Data.ImageUrl;
            var productUpdateDto = new ProductUpdateDto
            {
                Id = result!.Data.Id,
                Name = result!.Data.Name!,
                Properties = result!.Data.Properties!,
                Price = result!.Data.Price,
                IsHome = result!.Data.IsHome,
                CategoryIds = result!.Data.Categories.Select(x => x.Id).ToList()
            };
            return View(productUpdateDto);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(ProductUpdateDto productUpdateDto, string oldImageUrl)
    {
        if (!ModelState.IsValid)
        {
            var categories = await GetCategoriesAsync();
            if (categories is null)
            {
                _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoryList = categories;
            if (productUpdateDto.CategoryIds is null)
            {
                ModelState.AddModelError("CategoryIds", "En az bir kategori seçilmelidir!");
            }
            ViewBag.OldImageUrl = oldImageUrl;
            return View(productUpdateDto);
        }
        var client = new HttpClient();
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var formData = new MultipartFormDataContent();
            formData.Add(new StringContent(productUpdateDto.Id.ToString()), "Id");
            formData.Add(new StringContent(productUpdateDto.Name), "Name");
            formData.Add(new StringContent(productUpdateDto.Properties), "Properties");
            formData.Add(new StringContent(productUpdateDto.Price.ToString()!), "Price");
            formData.Add(new StringContent(productUpdateDto.IsHome.ToString()!), "IsHome");
            if (productUpdateDto.CategoryIds.Count == 0)
            {
                ModelState.AddModelError("CategoryIds", "En az bir kategori seçilmelidir!");
                var categories = await GetCategoriesAsync();
                if (categories is null)
                {
                    _toastr.AddErrorToastMessage("Kategori listesi çekilirken, sorun oluştu. Bu yüzden ürün ekleme sayfası açılamıyor!");
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.CategoryList = categories;
                return View(productUpdateDto);
            }

            foreach (int id in productUpdateDto.CategoryIds) // 5,7,8,11
            {
                formData.Add(new StringContent(id.ToString()), "CategoryIds");
            }
            if (productUpdateDto.Image is not null)
            {
                formData.Add(new StreamContent(productUpdateDto.Image.OpenReadStream()), "Image", productUpdateDto.Image.FileName);
            }

            var response = await client.PutAsync("http://localhost:5289/products", formData);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<NoContentDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return View(productUpdateDto);
            }
            _toastr.AddSuccessToastMessage("İşlem başarılı!");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    public async Task<IActionResult> Trash(int id)
    {
        var client = new HttpClient();

        var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:5289/products/{id}");
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<ProductDto>>(responseContent);
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
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Trash(int id, [FromQuery] bool isSoft = true, [FromQuery] bool isDeleted = true)
    {
        var client = new HttpClient();
        var isSoftUri = isSoft ? "soft" : "hard";
        var request = new HttpRequestMessage(isSoft ? HttpMethod.Put : HttpMethod.Delete, $"http://localhost:5289/products/delete/{isSoftUri}?id={id}");
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
                return RedirectToAction(nameof(Index));
            }
            _toastr.AddSuccessToastMessage("İşlem başarıyla tamamlanmıştır!");
            return RedirectToAction(nameof(Index), new { isDeleted });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> UpdateIsHome(int id)
    {
        var client = new HttpClient();
        try
        {
            var authResult = await _httpContextAccessor.HttpContext!.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            var token = authResult.Properties?.Items["access_token"];
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.PutAsync($"http://localhost:5289/products/ishome?id={id}", null);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<NoContentDto>>(responseContent);
            if (!result!.IsSuccessful)
            {
                _toastr.AddErrorToastMessage(result!.Errors[0]);
                return RedirectToAction(nameof(Index));
            }
            _toastr.AddSuccessToastMessage("İşlem başarılı!");
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }
    private async Task<List<CategoryDto>> GetCategoriesAsync()
    {
        var client = new HttpClient();
        var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5289/categories/getall");
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
                return null!;
            }
            return result!.Data;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }
    }
}