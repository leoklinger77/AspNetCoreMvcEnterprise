using Enterprise.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Repository
{
    public interface  IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> FindProductPerSupplier(Guid id);
        Task<IEnumerable<Product>> FindProductSupplier();
        Task<Product> FindProductSupplier(Guid id);
    }
}
