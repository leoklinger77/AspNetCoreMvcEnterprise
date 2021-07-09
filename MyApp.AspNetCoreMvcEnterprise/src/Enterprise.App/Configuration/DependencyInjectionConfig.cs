using Enterprise.Business.Interfaces.Repository;
using Enterprise.Data.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Enterprise.App.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection Dependency(this IServiceCollection service)
        {
            //Repository
            service.AddScoped<ISupplierRepository, SupplierRepository>();
            service.AddScoped<IProductRepository, ProductRepository>();
            service.AddScoped<IAddressRepository, AddressRepository>();
                        
            return service;
        }
    }
}
