using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        Task<T?> GetByIdAsync(int id);
        Task<IReadOnlyList<T>> ListAllAsync();
        // It would be optional as it would be for individual item
        Task<T?> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec); 
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<bool> SaveAllAsync();
        bool Exists(int id);
    }
}
