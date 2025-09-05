namespace RealEstate.Messaging.Utils;
using System.Diagnostics.Contracts;
using System.Reflection;

using MassTransit;

using RealEstate.Messaging.Abstraction;

public static class RegisterConfigurableEvents
{
    public static void AddConfigurableEvents(this IRabbitMqBusFactoryConfigurator config, Assembly assembly)
    {
        Contract.Requires(assembly != null);

        var types = assembly!
            .GetTypes()
            .Where(type => type != null &&
                        type.IsClass &&
                        !type.IsAbstract &&
                        !type.IsGenericType &&
                        IsConfigurableConsumer(type))
            .ToArray();

        ConfigureEvents(config, types);
    }

    public static void ConfigureEvents(IRabbitMqBusFactoryConfigurator config, params Type[] types)
    {
        foreach (var type in types)
        {
            // Create an instance of the type
            var instance = Activator.CreateInstance(type);

            // Cast to the interface to call the method (if applicable)
            if (instance is not null and IConfigurableEvent myInterfaceInstance)
            {
                myInterfaceInstance.Configure(config);
            }
            //else
            //{
            //    // Alternatively, use MethodInfo.Invoke for non-interface methods or dynamic calls
            //    MethodInfo method = type.GetMethod("MyMethod");
            //    if (method != null)
            //    {
            //        method.Invoke(instance, null); // Pass null for no parameters
            //    }
            //}
        }
    }

    private static bool IsConfigurableConsumer(Type type)
    {
        return typeof(IConfigurableEvent).IsAssignableFrom(type);
    }
}
