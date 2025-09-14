using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneCase.API.Controllers.BaseController;
using PhoneCase.Business.Abstract;
using PhoneCase.Shared.Dtos.OrderDtos;

namespace PhoneCase.API.Controllers
{
    [Route("orders")]
    [ApiController]
    public class OrdersController : CustomControllerBase
    {
        private readonly IOrderService _orderManager;

        public OrdersController(IOrderService orderManager)
        {
            _orderManager = orderManager;
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> OrderNow(OrderNowDto orderNowDto)
        {
            orderNowDto.UserId = UserId;
            var response = await _orderManager.OrderNowAsync(orderNowDto);
            return CreateResult(response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetOrder([FromQuery] int orderId)
        {
            var response = await _orderManager.GetOrderAsync(orderId);
            return CreateResult(response);
        }
        [HttpGet("myorder")]
        [Authorize]
        public async Task<IActionResult> GetMyOrder([FromQuery] int orderId)
        {
            var response = await _orderManager.GetMyOrderAsync(orderId, UserId);
            return CreateResult(response);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeOrderStatus(ChangeOrderStatusDto changeOrderStatusDto)
        {
            var response = await _orderManager.ChangeOrderStatusAsync(changeOrderStatusDto);
            return CreateResult(response);
        }
        [HttpPut("cancel")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CancelOrder([FromQuery] int orderId)
        {
            var response = await _orderManager.CancelOrderAsync(orderId);
            return CreateResult(response);
        }
        [HttpGet("getall")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllOrders([FromQuery] OrderFiltersDto? orderFiltersDto)
        {
            var response = await _orderManager.GetAllAsync(orderFiltersDto!);
            return CreateResult(response);
        }
        [HttpGet("myorders")]
        [Authorize]
        public async Task<IActionResult> GetAllMyOrders([FromQuery] OrderFiltersDto? orderFiltersDto)
        {
            orderFiltersDto!.UserId = UserId;
            var response = await _orderManager.GetAllAsync(orderFiltersDto!);
            return CreateResult(response);
        }
    }
}
