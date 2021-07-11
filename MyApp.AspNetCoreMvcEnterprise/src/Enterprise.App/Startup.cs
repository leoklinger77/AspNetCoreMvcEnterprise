using Enterprise.App.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Net.Mail;

namespace Enterprise.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IHostEnvironment hostEnvironment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", true, true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", true, true)
                .AddEnvironmentVariables();

            if (hostEnvironment.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
            }

            Configuration = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddControllersWithViews().AddRazorRuntimeCompilation();

            services.Context(Configuration);
            services.Dependency();
            services.MvcConfiguration();
            services.AddIdentity();
            services.AddAuthentication()
                .AddGoogle(options =>
            {
                options.ClientId = Configuration.GetValue<string>("web:client_id");
                options.ClientSecret = Configuration.GetValue<string>("web:client_secret");
            });

            services.AddScoped<SmtpClient>(option =>
            {
                SmtpClient smtp = new SmtpClient()
                {
                    Host = Configuration.GetValue<string>("Email:ServerSMTP"),
                    Port = Configuration.GetValue<int>("Email:Port"),
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(Configuration.GetValue<string>("Email:UserName"), Configuration.GetValue<string>("Email:Password")),
                    EnableSsl = true
                };

                return smtp;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                //app.UseExceptionHandler("/Error/500");
                //app.UseStatusCodePagesWithRedirects("/erro/{0}");
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
                app.UseHsts();
            }
            app.AppMvc();
        }
    }
}
