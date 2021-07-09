using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Enterprise.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Enterprise.Data.Repository
{
    public class SupplierRepository : Repository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(DataContext context) : base(context) { }

        public async Task<Supplier> FindSupplierAndAddress(Guid id)
        {
            return await _dbSet
                .Include(x => x.Endereco)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Supplier> FindSupplierAndAddressAndProduct(Guid id)
        {
            return await _dbSet
                .Include(x => x.Endereco)
                .Include(x => x.Products)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
