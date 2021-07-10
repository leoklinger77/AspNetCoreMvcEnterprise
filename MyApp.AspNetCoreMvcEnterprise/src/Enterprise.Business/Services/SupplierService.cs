using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using Enterprise.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Insert(Supplier supplier)
        {
            if (!ExecutedValidation(new SupplierValidation(), supplier)) return false;

            return true;
        }

        public Task<bool> Update(Supplier supplier)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAddress(Address address)
        {
            throw new NotImplementedException();
        }
    }
}
