using GamesApi.Rawg.Services.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace GamesApi.Rawg.Services
{
    /// <summary>
    /// The connector to inject the services into the service collection
    /// </summary>
    public static class RawgServicesConnector
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddScoped<IRawgApiService, RawgApiService>();

            return services;
        }
    }
}
