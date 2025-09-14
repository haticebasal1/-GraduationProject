using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.MVC.ViewModels;
using PhoneCase.Shared.Dtos.FavoriteDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using System.Net.Http.Headers;

namespace PhoneCase.MVC.ViewComponents
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly HttpClient _client;

        public FavoriteViewComponent()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5289/") };
        }

        public async Task<IViewComponentResult> InvokeAsync(string userId)
        {
            // Kullanıcının token'ını cookie’den al
            var token = HttpContext.User.FindFirst("access_token")?.Value;
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return View(new List<FavoriteDto>()); // giriş yoksa 0 göster
            }

            _client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"favorites/getall/all?userId={userId}&includeProduct=true");
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<FavoriteDto>());
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseViewModel<List<FavoriteDto>>>(responseContent);

            return View(result?.Data ?? new List<FavoriteDto>());
        }
    }
}
