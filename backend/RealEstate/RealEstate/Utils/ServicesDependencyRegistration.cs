namespace RealEstate.Utils;

using System.Diagnostics.Contracts;
using System.Reflection;

using RealEstate.Shared.Abstraction;

public static class ServicesDependencyRegistration
{
    public static void AddAssembly(IServiceCollection services, Assembly assembly)
    {
        Contract.Requires(assembly != null);

        var types = assembly
            .GetTypes()
            .Where(type => type != null &&
                        type.IsClass &&
                        !type.IsAbstract &&
                        !type.IsGenericType &&
                        GetLifeTime(type) != null)
            .ToArray();

        AddTypes(services, types);
    }

    public static void AddTypes(IServiceCollection services, params Type[] types)
    {
        foreach (var type in types)
        {
            AddType(services, type);
        }
    }

    public static void AddType(IServiceCollection services, Type type)
    {
        Contract.Requires(services != null);

        var lifeTime = GetLifeTime(type);

        if (lifeTime == null)
        {
            return;
        }

        var serviceDescriptor = CreateServiceDescriptor(
            type,
            lifeTime.Value
        );

        services!.Add(serviceDescriptor);
    }

    private static ServiceLifetime? GetLifeTime(Type type)
    {
        if (typeof(ITransientDependency).IsAssignableFrom(type))
        {
            return ServiceLifetime.Transient;
        }

        if (typeof(ISingletonDependency).IsAssignableFrom(type))
        {
            return ServiceLifetime.Singleton;
        }

        if (typeof(IScopedDependency).IsAssignableFrom(type))
        {
            return ServiceLifetime.Scoped;
        }

        return null;
    }

    private static ServiceDescriptor CreateServiceDescriptor(
            Type implementationType,
            ServiceLifetime lifeTime)
    {
        var isService = typeof(IService).IsAssignableFrom(implementationType);
        if (isService)
        {
            var interfaceName = $"I{implementationType.Name}";
            var interfaceType = implementationType.GetInterface(interfaceName);

            if (interfaceType == null)
                throw new InvalidOperationException($"{implementationType.Name} is not implementing any interface");

            return ServiceDescriptor.Describe(
                interfaceType,
                implementationType,
                lifeTime
            );
        }
        else
        {
            return ServiceDescriptor.Describe(
                implementationType,
                implementationType,
                lifeTime
            );
        }
    }
}
