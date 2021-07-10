using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Insert(Product product)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(Product product)
        {
            throw new NotImplementedException();
        }
    }

}
