using Domain.Contracts;
using Domain.Repository;
using Infrastructure.Repositories;
using JeBalance.Repos;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDenonciationRepository, DenonciationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();


            return services;
        }
    }
}
