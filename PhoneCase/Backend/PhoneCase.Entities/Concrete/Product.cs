using System;
using PhoneCase.Entities.Abstract;

namespace PhoneCase.Entities.Concrete;

public class Product : BaseEntity, IEntity
{
    private Product()
    {

    }
    public Product(string? name, string? properties, decimal price, string? imageUrl, bool isHome)
    {
        Name = name;
        Properties = properties;
        Price = price;
        ImageUrl = imageUrl;
        IsHome = isHome;
    }

    public string? Name { get; set; }
    public string? Properties { get; set; }
    public decimal Price { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsHome { get; set; }
    public ICollection<ProductCategory> ProductCategories { get; set; } = [];
}
