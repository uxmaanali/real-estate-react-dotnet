using System.Reflection;

using RealEstate.Database;
using RealEstate.Services;
using RealEstate.Shared.Abstraction;

namespace RealEstate.Utils;

public static class ServiceDependencyInjection
{
    public static IServiceCollection AddServicsDependency(this IServiceCollection services)
    {
        var assemblies = new List<Assembly> { typeof(DatabaseSetup).Assembly, typeof(Program).Assembly, typeof(ServicesSetup).Assembly, typeof(AuthService).Assembly };
        assemblies.ForEach(assembly => ServicesDependencyRegistration.AddAssembly(services, assembly));

        return services;
    }
}
