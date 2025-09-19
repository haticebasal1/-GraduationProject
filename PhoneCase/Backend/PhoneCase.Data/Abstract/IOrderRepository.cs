using System;
using PhoneCase.Shared.Dtos.OrderDtos;

namespace PhoneCase.Data.Abstract;

public interface IOrderRepository
{
 decimal GetOrdersTotal(OrderFiltersDto orderFiltersDto);
}
