using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Models;
using Enterprise.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(DataContext context) : base(context) { }

        public async Task<IEnumerable<Product>> FindProductPerSupplier(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> FindProductSupplier()
        {
            return await _dbSet.Include(x => x.Supplier).AsNoTracking().ToListAsync();
        }

        public async Task<Product> FindProductSupplier(Guid id)
        {
            return await _dbSet.Where(x => x.Id == id).Include(x => x.Supplier).AsNoTracking().FirstOrDefaultAsync();
        }
    }
}
