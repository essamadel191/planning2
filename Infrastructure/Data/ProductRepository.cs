using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class ProdcutRepository(StoreContext context) : IProductRepository
{
    //private readonly StoreContext context = context;
    public void Addproduct(Products product)
    {
        context.Products.Add(product);
    }

    public void DeleteProduct(Products product)
    {
        context.Products.Remove(product);
    }

    public async Task<IReadOnlyList<string>> GetBrandsAsync()
    { 
        return await context.Products.Select(x => x.Brand)
                    .Distinct()
                    .ToListAsync();
    }

   public async Task<IReadOnlyList<string>> GetTypesAsync()
    { 
        return await context.Products.Select(x => x.Type)
                    .Distinct()
                    .ToListAsync();
    }

    public async Task<IReadOnlyList<Products>> GetProductAsync(string? brand,string? type,string? sort)
    {
        var query = context.Products.AsQueryable();

        if(!string.IsNullOrWhiteSpace(brand))
            query = query.Where(x => x.Brand == brand);
        
        if(!string.IsNullOrWhiteSpace(type))
            query = query.Where(x => x.Type == type);

        
        query = sort switch
        {
            "priceAsc" => query.OrderBy(x => x.Price),
            "proceDesc" => query.OrderByDescending(x => x.Price),
            _ => query.OrderBy(x => x.Name)
        };
        
        
        return await query.ToListAsync();
    }

    public async Task<Products?> GetProductByIdAsync(int id)
    {
        return await context.Products.FindAsync(id);
    }


    public bool ProductExists(int id)
    {
        return context.Products.Any(x => x.Id == id);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void UpdateProduct(Products product)
    {
        context.Entry(product).State = EntityState.Modified;
    }
}
