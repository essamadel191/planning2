using System;
using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Products>
{
    public ProductSpecification(string? brand, string? type, string? sort) : base(x => 
    
        (string.IsNullOrWhiteSpace(brand) || x.Brand == brand) &&
        (string.IsNullOrWhiteSpace(type) || x.Type == type)
    )
    {
        switch (sort)
        {
            case "priceAsc":
                AddOrderBy(x => x.Price);
                break;
            
            case "priceDesc":
                AddOrdetByDescending(x => x.Price);
                break;
            
            // Sort Alphabetically
            default:
                AddOrderBy(x => x.Name);
                break;
        }
    }
}
