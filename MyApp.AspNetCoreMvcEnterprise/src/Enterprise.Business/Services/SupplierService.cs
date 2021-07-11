using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using Enterprise.Business.Models.Validations;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Enterprise.Business.Services
{
    public class SupplierService : BaseService, ISupplierService
    {
        private readonly ISupplierRepository _supplierRepository;
        private readonly IAddressRepository _addressRepository;

        public SupplierService(INotification notification, ISupplierRepository supplierRepository, 
                                IAddressRepository addressRepository) : base(notification)
        {
            _supplierRepository = supplierRepository;
            _addressRepository = addressRepository;
        }

        public async Task<bool> Insert(Supplier supplier)
        {
            if (!ExecutedValidation(new SupplierValidation(), supplier)) return false;
            
            if(_supplierRepository.Find(x => x.Documento == supplier.Documento).Result.Any())
            {
                Notifier("Já existe um Fornecedor com esse documento cadastrado");
                return false;
            }
            await _supplierRepository.Insert(supplier);
            return true;
        }

        public async Task<bool> Update(Supplier supplier)
        {
            if (!ExecutedValidation(new SupplierValidation(), supplier)) return false;

            if (_supplierRepository.Find(x => x.Documento == supplier.Documento && x.Id != supplier.Id).Result.Any())
            {
                Notifier("Já existe um Fornecedor com esse documento cadastrado");
                return false;
            }
            await _supplierRepository.Update(supplier);
            return true;
        }

        public async Task<bool> UpdateAddress(Address address)
        {
            if (!ExecutedValidation(new AddressValidation(), address)) return false;

            await _addressRepository.Update(address);

            return true;
        }
        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var rs = await _supplierRepository.FindSupplierAndAddressAndProduct(id);
                if (rs.Products.Any())
                {
                    Notifier("O fornecedor possui produtos cadastrados.");
                    return false;
                }

                if (rs.Endereco != null) await _addressRepository.Delete(rs.Endereco.Id);

                await _supplierRepository.Delete(id);

                return true;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            
        }

        public void Dispose()
        {
            _addressRepository?.Dispose();
            _supplierRepository?.Dispose();
        }
    }
}
