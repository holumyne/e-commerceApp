using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context;
        internal DbSet<T> dbSet;

        public GenericRepository(StoreContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>(); //this means _db.Categories == dbSet
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> ListAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public void Update(T entity)
        {
            dbSet.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(dbSet.AsQueryable(), spec);
        }
    }
}
