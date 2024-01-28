using System;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Domain.Service;
namespace API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));


            return services;
        }
    }
}
