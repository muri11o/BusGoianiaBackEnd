using BusGoiania.MainAPI.HttpClients;
using BusGoiania.MainAPI.Interfaces;
using BusGoiania.MainAPI.Notifications;

namespace BusGoiania.MainAPI.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<IMiddlewareHttpClient, MiddlewareHttpClient>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
