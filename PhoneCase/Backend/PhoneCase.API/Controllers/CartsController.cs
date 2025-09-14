using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.CartDtos;
using PhoneCase.Shared.Dtos.CartDtos.ChangeQuantityDto;

namespace PhoneCase.API.Controllers
{
    [Route("carts")]
    [ApiController]
    [Authorize]
    public class CartsController : CustomControllerBase
    {
        private readonly ICartService _cartManager;

        public CartsController(ICartService cartManager)
        {
            _cartManager = cartManager;
        }
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var response = await _cartManager.GetCartAsync(UserId);
            return CreateResult(response);
        }
        [HttpPost]
        public async Task<IActionResult> AddToCart(AddToCartDto addToCartDto)
        {
            addToCartDto.UserId = UserId;
            var response = await _cartManager.AddToCartAsync(addToCartDto);
            return CreateResult(response);
        }
        [HttpPut]
        public async Task<IActionResult> ChangeQuantity(ChangeQuantityDto changeQuantityDto)
        {
            var response = await _cartManager.ChangeQuantityAsync(changeQuantityDto);
            return CreateResult(response);
        }
        [HttpPut("qty/{cartItemId}")]
        public async Task<IActionResult> ChangeQuantitAlternativey(int cartItemId, [FromQuery] int quantity)
        {
            var changeQuantityDto = new ChangeQuantityDto { CartItemId = cartItemId, Quantity = quantity };
            var response = await _cartManager.ChangeQuantityAsync(changeQuantityDto);
            return CreateResult(response);
        }
        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> ChangeQuantity(int cartItemId)
        {
            var response = await _cartManager.RemoveFromCartAsync(cartItemId);
            return CreateResult(response);
        }
        [HttpDelete]
        public async Task<IActionResult> Clear()
        {
            var response = await _cartManager.ClearCartAsync(UserId);
            return CreateResult(response);
        }
    }
}
