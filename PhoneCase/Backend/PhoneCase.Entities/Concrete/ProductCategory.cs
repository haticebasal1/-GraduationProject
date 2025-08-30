using System;
using PhoneCase.Entities.Abstract;

namespace PhoneCase.Entities.Concrete;

public class ProductCategory : IEntity
{
    private ProductCategory()
    {
        
    }
    public ProductCategory(int productId, int categoryId)
    {
        ProductId = productId;
        CategoryId = categoryId;
    }

    public int ProductId { get; set; }
    public Product? Product { get; set; }
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
}

