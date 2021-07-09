using Enterprise.Business.Models;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Repository
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
        Task<Supplier> FindSupplierAndAddress(Guid id);
        Task<Supplier> FindSupplierAndAddressAndProduct(Guid id);
    }
}
