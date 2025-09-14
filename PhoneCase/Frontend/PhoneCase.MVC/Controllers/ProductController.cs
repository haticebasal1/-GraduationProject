using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.MVC.ViewModels;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.MVC.Controllers;

public class ProductController : Controller
{
    public async Task<IActionResult> Index(int? categoryId = null)
    {
        ViewBag.CategoryId = categoryId;
        return View();
    }

[Authorize]
    public async Task<IActionResult> Details(string id)
    {
        var client = new HttpClient();
        var requestUrl = $"http://localhost:5289/products/{id}?includeCategories=true";

        try
        {
            var response = await client.GetAsync(requestUrl);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }
            var result = JsonConvert.DeserializeObject<ResponseViewModel<ProductDto>>(responseContent);

            if (result?.Data == null)
                return NotFound();

            return View(result.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            return StatusCode(500, "Sunucu hatası oluştu.");
        }
    }

}
