using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Ordering.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; // Add this using directive

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("Database");
            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
            services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
           
            // Add services to the container
            services.AddDbContext<ApplicationDbContext>((sp ,options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            }); // Fix: use 'options' and ensure correct extension method

            //services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
            return services;
        }
    }
}