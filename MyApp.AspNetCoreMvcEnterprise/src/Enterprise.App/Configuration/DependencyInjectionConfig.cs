using Enterprise.App.Extensions;
using Enterprise.Business.Interfaces;
using Enterprise.Business.Interfaces.Repository;
using Enterprise.Business.Interfaces.Service;
using Enterprise.Business.Notifications;
using Enterprise.Business.Services;
using Enterprise.Data.Repository;
using Microsoft.AspNetCore.Identity.UI.Services;
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

            //Notification
            service.AddScoped<INotification, Notification>();

            //Services
            service.AddScoped<ISupplierService, SupplierService>();
            service.AddScoped<IProductService, ProductService>();

            service.AddTransient<IEmailSender, SendEmail>();


            return service;
        }
    }
}
