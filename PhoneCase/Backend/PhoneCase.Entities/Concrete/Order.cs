using System;
using PhoneCase.Entities.Abstract;
using PhoneCase.Shared.Enums;

namespace PhoneCase.Entities.Concrete;

public class Order : BaseEntity, IEntity
{
    public Order(string? userId, string? address, string? city)
    {
        UserId = userId;
        Address = address;
        City = city;
    }

    public string? UserId { get; set; }
    public User? User { get; set; }
    public string? Address { get; set; }
    public string? City { get; set; }
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    public ICollection<OrderItem> OrderItems { get; set; } = [];
}
