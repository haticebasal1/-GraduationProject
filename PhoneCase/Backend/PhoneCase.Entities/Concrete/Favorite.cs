using System;
using PhoneCase.Entities.Abstract;

namespace PhoneCase.Entities.Concrete;

public class Favorite : BaseEntity, IEntity
{
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public User? User { get; set; }
    public Product? Product { get; set; }
}
