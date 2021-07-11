using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Enterprise.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enterprise.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DataContext _context;
        protected readonly DbSet<T> _dbSet;

        protected Repository(DataContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<ICollection<T>> Find(Expression<Func<T, bool>> expression)
        {
            return await _dbSet.Where(expression).AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> FindById(Guid id)
        {
            return await _dbSet.AsNoTracking().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public virtual async Task<ICollection<T>> FindAll()
        {
            return await _dbSet.AsNoTracking().AsNoTracking().ToListAsync();
        }

        public virtual async Task Insert(T entity)
        {
            await _dbSet.AddAsync(entity);
            await SaveChange();
        }

        public virtual async Task Update(T entity)
        {
            _dbSet.Update(entity);
            await SaveChange();
        }

        public virtual async Task Delete(Guid id)
        {
            _dbSet.Remove(new T { Id = id });
            await SaveChange();
        }

        public async Task<int> SaveChange()
        {
            return await _context.SaveChangesAsync();
        }
        public void Dispose()
        {
            _context?.DisposeAsync();
        }
    }
}
