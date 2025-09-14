using System;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Shared.Dtos.OrderDtos;

public class OrderFiltersDto
{
    public OrderFiltersDto(){}
    public OrderFiltersDto(OrderStatus? orderStatus = null, string? userId = null, DateTime? startDate = null, DateTime? endDate = null, bool? isDeleted = null)
    {
        OrderStatus = orderStatus;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        IsDeleted = isDeleted;
    }

    public OrderStatus? OrderStatus { get; set; } = null;
    public string? UserId { get; set; } = null;
    public DateTime? StartDate { get; set; } = null;
    public DateTime? EndDate { get; set; } = null;
    public bool? IsDeleted { get; set; } = null;
}
