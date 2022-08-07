using BusGoiania.MainAPI.HttpClients;
using BusGoiania.MainAPI.Interfaces;
using BusGoiania.MainData.Context;
using BusGoiania.MainData.Repository;
using BusGoiania.MainDomain.Interfaces;
using BusGoiania.MainDomain.Notifications;
using BusGoiania.MainDomain.Services;
using Microsoft.EntityFrameworkCore;

namespace BusGoiania.MainAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
            x => x.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<INumeroPontoFavoritoRepository, NumeroPontoFavoritoRepository>();
            services.AddScoped<INumeroPontoFavoritoService, NumeroPontoFavoritoService>();

            services.AddHttpClient<IMiddlewareHttpClient, MiddlewareHttpClient>();

            return services;
        }
    }
}
