using Enterprise.Business.Models;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Interfaces.Repository
{
    public interface IAddressRepository : IRepository<Address>
    {
        Task<Address> FindAddressPerSupplier(Guid id);
    }
}
