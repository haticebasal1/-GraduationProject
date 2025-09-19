using System.Net.Http.Headers;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PhoneCase.Shared.Dtos.FavoriteDtos;
using PhoneCase.Shared.Dtos.ProductDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.MVC.Controllers
{
    public class FavoriteController : Controller
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FavoriteController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var accessToken = User.FindFirst("access_token")?.Value;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(accessToken))
                return RedirectToAction("Login", "Auth", new { returnUrl = Url.Action("Index", "Favorite") });

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.GetAsync($"http://localhost:5289/favorites/getall/all?userId={userId}&includeProduct=true");

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.FavoriteCount = 0;
                return View(new List<FavoriteDto>());
            }

            var content = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ResponseDto<List<FavoriteDto>>>(content);

            var favorites = result?.Data ?? new List<FavoriteDto>();
            ViewBag.FavoriteCount = favorites.Count;
            return View(favorites);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(int productId)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var accessToken = User.FindFirst("access_token")?.Value;

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var dto = new FavoriteCreateDto
            {
                UserId = userId!,
                ProductId = productId
            };

            // ✅ Doğru endpoint: sadece /favorites
            var response = await client.PostAsJsonAsync("http://localhost:5289/favorites", dto);

            if (!response.IsSuccessStatusCode)
            {
                TempData["Error"] = "Favori eklenirken bir hata oluştu.";
                return RedirectToAction("Index", "Product");
            }

            return RedirectToAction("Index", "Favorite");
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Remove(int favoriteId)
        {
            var accessToken = User.FindFirst("access_token")?.Value;

            if (string.IsNullOrEmpty(accessToken))
                return RedirectToAction("Login", "Auth", new { returnUrl = Url.Action("Index", "Favorite") });

            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var response = await client.DeleteAsync($"http://localhost:5289/favorites/delete/hard?id={favoriteId}");

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();
                // burada loglama yapabilirsin
            }

            return RedirectToAction("Index");
        }

        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            using var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5289/products/{id}?includeCategory=true");

            if (!response.IsSuccessStatusCode)
                return NotFound();

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ResponseDto<ProductDto>>(content)?.Data;

            if (product == null)
                return NotFound();

            return View(product);
        }
    }
}
