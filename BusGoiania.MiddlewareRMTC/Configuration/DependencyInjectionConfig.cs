using BusGoiania.MiddlewareRMTC.HttpClients;
using BusGoiania.MiddlewareRMTC.Interfaces;
using BusGoiania.MiddlewareRMTC.Notifications;

namespace BusGoiania.MiddlewareRMTC.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddHttpClient<IRmtcHttpClient, RmtcHttpClient>();
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
