using Core.Entities;
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
        private readonly StoreContext storeContext;

        public GenericRepository(StoreContext storeContext)
        {
            this.storeContext = storeContext;   
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            var s = ApplySpecification(spec);
                
                return await s.CountAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await storeContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsync(ISpecification<T> spec) => await ApplySpecification(spec).ToListAsync();

        public async Task<T> GetByIdAsync(int id)
        {
            return await storeContext.Set<T>().FirstAsync();
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec) => await ApplySpecification(spec).FirstOrDefaultAsync();

        private IQueryable<T> ApplySpecification(ISpecification<T> spec) => SpecificationEvaluator<T>.GetQuery(storeContext.Set<T>().AsQueryable(), spec);

    }
}
