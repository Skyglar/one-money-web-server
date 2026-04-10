using Transactions.Application;
using Transactions.Infrastructure;

namespace Transactions.API.Extensions;

public static class Extensions {
    public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IApplicationAssemblyMarker).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(IInfrastructureAssemblyMarker).Assembly);
        });
        
        return services;
    }
}