using GamesApi.UsersRepository.Abstracts;
using Microsoft.Extensions.DependencyInjection;

namespace GamesApi.UsersRepository
{
    public static class RepositoryConnectors
    {
        public static IServiceCollection Configure(IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();

            return services;
        }
    }
}
