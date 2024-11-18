using System;
using System.Text.Json;
using Core.Entities;

namespace Infrastructure.Data;

public class StoreContextSeed
{
    // Static mean that we can use this method without the need
    // of creating new instance of this class
    public static async Task SeedAsync(StoreContext context)
    {
        if(!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Products>>(productsData);

            if(products == null) return;

            context.Products.AddRange(products);

            await context.SaveChangesAsync();
        }
    }
}
