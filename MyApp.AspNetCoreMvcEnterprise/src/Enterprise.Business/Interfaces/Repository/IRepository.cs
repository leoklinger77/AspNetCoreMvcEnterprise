using Enterprise.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Repository
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        Task Insert(T entity);
        Task Update(T entity);
        Task Delete(Guid id);
        Task<T> FindById(Guid id);
        Task<IEnumerable<T>> FindAll(T entity);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<int> SaveChange();
    }
}
