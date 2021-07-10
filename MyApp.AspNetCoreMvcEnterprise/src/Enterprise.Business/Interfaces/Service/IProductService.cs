using Enterprise.Business.Models;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Service
{
    public interface IProductService : IDisposable
    {
        Task<bool> Insert(Product product);
        Task<bool> Update(Product product);
        Task<bool> Delete(Guid id);
    }
}
