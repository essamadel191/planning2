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

    public async Task<IReadOnlyList<Products>> GetProductAsync()
    {
        return await context.Products.ToListAsync();
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
