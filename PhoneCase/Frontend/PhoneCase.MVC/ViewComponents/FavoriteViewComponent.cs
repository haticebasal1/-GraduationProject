using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.Shared.Dtos.FavoriteDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace PhoneCase.MVC.ViewComponents
{
    public class FavoriteViewComponent : ViewComponent
    {
        private readonly HttpClient _client;

        public FavoriteViewComponent()
        {
            _client = new HttpClient { BaseAddress = new Uri("http://localhost:5289/") };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var token = HttpContext.User.FindFirst("access_token")?.Value;

            // Giriş yoksa boş liste döndür (view bu listeyi kullanır)
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return View(new List<FavoriteDto>());
            }

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _client.GetAsync($"favorites/getall/all?userId={userId}&includeProduct=true");

            if (!response.IsSuccessStatusCode)
            {
                return View(new List<FavoriteDto>());
            }

            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<FavoriteDto>>>(responseContent);

            var list = result?.Data ?? new List<FavoriteDto>();
            return View(list);
        }
    }
}
