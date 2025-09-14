using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.MVC.ViewModels;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.MVC.ViewComponents;

public class ProductsViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(int? categoryId=null)
    {
        var client = new HttpClient();
        var url = categoryId is null ? "http://localhost:5289/products":"http://localhost:5289/products?categoryId={categoryId}";
        var request = new HttpRequestMessage(HttpMethod.Get,url);
        try
        {
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseViewModel<List<ProductDto>>>(responseContent);

            if (result is not null)
            {
                response.EnsureSuccessStatusCode();
            }
            return View(result!.Data);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Hata: {ex.Message}");
            throw;
        }

    }
}