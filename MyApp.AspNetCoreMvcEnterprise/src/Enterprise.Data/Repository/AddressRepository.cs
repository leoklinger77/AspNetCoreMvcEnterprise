using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Enterprise.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Enterprise.Data.Repository
{
    public class AddressRepository : Repository<Address>, IAddressRepository
    {
        public AddressRepository(DataContext context) : base(context) { }

        public async Task<Address> FindAddressPerSupplier(Guid id)
        {
            return await _dbSet.Include(x => x.Supplier).AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
