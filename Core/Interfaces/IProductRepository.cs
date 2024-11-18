using System;
using Core.Entities;

namespace Core.Interfaces;

public interface IProductRepository
{
    // why I read only that mean that
    // I won't modify this list of products
    Task<IReadOnlyList<Products>> GetProductAsync();
    Task<Products?> GetProductByIdAsync(int id);
    void Addproduct(Products product);
    void UpdateProduct(Products product);
    void DeleteProduct(Products product);
    bool ProductExists(int id);
    Task<bool> SaveChangesAsync();
}
