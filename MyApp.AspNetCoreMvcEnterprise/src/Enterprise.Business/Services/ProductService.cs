using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Models;
using Enterprise.Business.Models.Validations;
using System;
using System.Threading.Tasks;

namespace Enterprise.Business.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(INotification notification, IProductRepository productRepository) : base(notification)
        {
            _productRepository = productRepository;
        }

        public async Task<bool> Delete(Guid id)
        {
            await _productRepository.Delete(id);
            return true;
        }
                
        public async Task<bool> Insert(Product product)
        {
            if (!ExecutedValidation(new ProductValidation(), product)) return false;
            
            await _productRepository.Insert(product);
            return true;
        }

        public async Task<bool> Update(Product product)
        {
            if (!ExecutedValidation(new ProductValidation(), product)) return false;

            await _productRepository.Update(product);
            return true;
        }
        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
