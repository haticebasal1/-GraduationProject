using System;
using PhoneCase.Entities.Abstract;

namespace PhoneCase.Entities.Concrete;

public class Cart:BaseEntity,IEntity
{
    private Cart() { }

    public string? UserId { get; set; }
    public Cart(string? userId)
    {
        UserId = userId;
    }

    public User? User { get; set; }
    public ICollection<CartItem> CartItems { get; set; } = [];
}
