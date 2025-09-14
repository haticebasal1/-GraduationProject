using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.FavoriteDtos;

namespace PhoneCase.API.Controllers
{
    [Route("favorites")]
    [ApiController]
    [Authorize]
    public class FavoritesController : CustomControllerBase
    {
        private readonly IFavoriteService _favoriteManager;

        public FavoritesController(IFavoriteService favoriteManager)
        {
            _favoriteManager = favoriteManager;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] FavoriteCreateDto favoriteCreateDto)
        {
            var response = await _favoriteManager.AddAsync(favoriteCreateDto);
            return CreateResult(response);
        }
        [HttpGet("getall/all")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? userId = null!,
            [FromQuery] int productId = 0,
            [FromQuery] bool includeUser = false,
            [FromQuery] bool includeProduct = true, // artık true olmalı
            [FromQuery] bool? isDeleted = null)
        {
            var response = await _favoriteManager.GetAllAsync(
                userId: userId!,
                productId: productId,
                includeUser: includeUser,
                includeProduct: includeProduct,
                isDeleted: isDeleted
            );
            return CreateResult(response);
        }
        [HttpGet("getall/deleted")]
        public async Task<IActionResult> GetAllDeleted()
        {
            var response = await _favoriteManager.GetAllAsync(isDeleted: true);
            return CreateResult(response);
        }
        [HttpGet("getall/active")]
        public async Task<IActionResult> GetAllActive()
        {
            var response = await _favoriteManager.GetAllAsync(isDeleted: false);
            return CreateResult(response);
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _favoriteManager.GetByIdAsync(id);
            return CreateResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] FavoriteUpdateDto favoriteUpdateDto)
        {
            var response = await _favoriteManager.UpdateAsync(favoriteUpdateDto);
            return CreateResult(response);
        }
        [HttpDelete("delete/hard")]
        public async Task<IActionResult> HardDelete([FromQuery] int id)
        {
            var response = await _favoriteManager.DeleteAsync(id);
            return CreateResult(response);
        }
        [HttpPut("delete/soft")]
        public async Task<IActionResult> SoftDelete(int id)
        {
            var response = await _favoriteManager.SoftDeletedAsync(id);
            return CreateResult(response);
        }
    }
}
