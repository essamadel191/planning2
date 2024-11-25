using System;
using Core.Entities;

namespace Core.Specfications;

public class ProductSpecification : BaseSpecfication<Products>
{
    public ProductSpecification(string? brand, string? type) : base(x => 
    
        (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
        (string.IsNullOrWhiteSpace(type) || x.Type == type)
    )
    {
        
    }
}
