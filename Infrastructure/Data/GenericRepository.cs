using System;
using Core.Entities;
using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
{
    private readonly StoreContext context = context;

    public async Task<T?> GetByIdAsync(int id)
    {
        return await context.Set<T>().FindAsync(id);
    }
    public async Task<IReadOnlyList<T>> ListAllAsync()
    {
        return await context.Set<T>().ToListAsync();
    }
    public void Add(T entity)
    {
        context.Set<T>().Add(entity);    
    }
    public void Update(T entity)
    {
        context.Set<T>().Attach(entity);
        context.Entry(entity).State = EntityState.Modified;
    }
    public void Remove(T entity)
    {
        context.Set<T>().Remove(entity);    
    }
    public async Task<bool> SaveAllAsync()
    {
        return await context.SaveChangesAsync() > 0; 
    }
    public bool Exists(int id)
    {
        return context.Set<T>().Any(x => x.Id == id);
    }

}
