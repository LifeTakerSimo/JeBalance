using Domain.Contracts;
using Domain.Model;
using Domain.Queries.Users;
using Domain.Repository;
using Domain.Service;
using Infrastructure.Repositories;
using JeBalance.Repos;
using JeBalance.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IDenonciationRepository, DenonciationRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordService, PasswordService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IPersonRepository, PersonRepository>();

            return services;
        }
    }
}
