using Enterprise.App.Extensions;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Data.Repository;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
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

            //Coin Validation
            service.AddSingleton<IValidationAttributeAdapterProvider, CoinValidationAttributeAdapterProvider>();
                        
            return service;
        }
    }
}
