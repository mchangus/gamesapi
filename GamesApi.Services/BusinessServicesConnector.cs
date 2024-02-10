using Games.Services.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace Games.Services
{
    /// <summary>
    /// The connector to inject the services into the service collection
    /// </summary>
    public static class BusinessServicesConnector
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddScoped<IGamesService, GamesService>();
            services.AddScoped<IUsersService, UsersService>();

            return services;
        }
    }
}
