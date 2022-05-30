using BusGoiania.AuthProvider.Interfaces;
using BusGoiania.AuthProvider.Notifications;

namespace BusGoiania.AuthProvider.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependenciesAuth(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();

            return services;
        }
    }
}
