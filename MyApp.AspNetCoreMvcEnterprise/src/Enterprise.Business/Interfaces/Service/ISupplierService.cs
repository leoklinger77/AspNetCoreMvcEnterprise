using Enterprise.Business.Models;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Service
{
    public interface ISupplierService : IDisposable
    {
        Task<bool> Insert(Supplier supplier);
        Task<bool> Update(Supplier supplier);
        Task<bool> Delete(Guid id);

        Task<bool> UpdateAddress(Address address);
    }
}
