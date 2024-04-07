using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            _storeContext = storeContext;   
        }

        public void Add(T entity)
        {
            _storeContext.Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var s = ApplySpecification(spec);
                
            return await s.CountAsync();
        }

        public void Delete(T entity)
        {
            _storeContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec) => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        {
            return await _storeContext.Set<T>().FirstAsync(o => o.Id == id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

        public void Update(T entity)
        {
            _storeContext.Set<T>().Attach(entity);
            _storeContext.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQuery(_storeContext.Set<T>().AsQueryable(), spec);

    }
}
