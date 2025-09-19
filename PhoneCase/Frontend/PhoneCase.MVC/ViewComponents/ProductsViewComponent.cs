using PhoneCase.MVC.Models;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PhoneCase.MVC.ViewComponents;

public class ProductsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int? categoryId = null)
    {
        var client = new HttpClient();
        var url = categoryId is null 
            ? "http://localhost:5289/products" 
            : $"http://localhost:5289/products?categoryId={categoryId}";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        try
        {
            var response = await client.SendAsync(request);

            // Burada önce status code kontrol et
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<ProductDto>>>(responseContent);

            return View(result!.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            return View(new List<ProductDto>()); // Hata olursa boş liste dön
        }
    }
}
