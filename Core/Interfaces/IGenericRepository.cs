using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public Task<T> GetByIdAsync(int id);
        public Task<IReadOnlyList<T>> GetAllAsync();

        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(T entity);
    }
}
