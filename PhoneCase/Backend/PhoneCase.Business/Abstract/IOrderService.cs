using System;
using PhoneCase.Shared.Dtos.OrderDtos;
using PhoneCase.Shared.Dtos.ResponseDtos;

namespace PhoneCase.Business.Abstract;

public interface IOrderService
{
    Task<ResponseDto<OrderDto>> OrderNowAsync(OrderNowDto orderNowDto);
    Task<ResponseDto<NoContentDto>> ChangeOrderStatusAsync(ChangeOrderStatusDto changeOrderStatusDto);
    Task<ResponseDto<OrderDto>> GetOrderAsync(int id);
    Task<ResponseDto<OrderDto>> GetMyOrderAsync(int id,string userId);
    Task<ResponseDto<NoContentDto>> CancelOrderAsync(int id);
    Task<ResponseDto<IEnumerable<OrderDto>>> GetAllAsync(OrderFiltersDto orderFiltersDto);
}
