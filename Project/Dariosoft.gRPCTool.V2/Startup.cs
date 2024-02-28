using Microsoft.Extensions.DependencyInjection;

namespace Dariosoft.gRPCTool.V2
{
    public static class Startup
    {
        public static IServiceCollection AddgRPCTool(this IServiceCollection services, Action<Options> configure)
        {
            var allTypes = typeof(Startup).Assembly.GetTypes();

            return services
                .RegisterComonentComposers(allTypes)
                .RegisterOf(typeof(ElementNameStrategies.INameGenerateStrategy), allTypes)
                .RegisterOf(typeof(TypeRefineries.ITypeRefiner), allTypes)
                
                .AddSingleton<Utilities.IAssemblyLoader, Utilities.AssemblyLoader>()
                .AddSingleton<Utilities.ILogger, Utilities.ConsoleLogger>()
                
                .AddSingleton<Factories.IProtobufComponentFactory, Factories.ProtobufComponentFactory>()
                .AddSingleton<Factories.INameFactory, Factories.NameFactory>()
                .AddSingleton<Factories.IXTypeFactory, Factories.XTypeFactory>()
                
                .AddSingleton<ElementNameStrategies.ServiceNameStrategy>()
                
                .AddSingleton<TypeRefineries.ITypeRefinery, TypeRefineries.TypeRefinery>()
                .AddSingleton<IEngine, Engine>()
                .Configure(configure);
        }

        private static IServiceCollection RegisterComonentComposers(this IServiceCollection services, IEnumerable<Type> types)
        {
            var baseType = typeof(Composers.ComponentComposer);

            var composerTypes = types.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(baseType)).ToArray();

            for (var i = 0; i < composerTypes.Length; i++)
            {
                services.AddSingleton(composerTypes[i]);
            }

            return services;
        }
        
        
        private static IServiceCollection RegisterOf(this IServiceCollection services, Type baseType, IEnumerable<Type> types)
        {

            var composerTypes = types.Where(t => t is { IsClass: true, IsAbstract: false } && t.IsAssignableTo(baseType)).ToArray();

            for (var i = 0; i < composerTypes.Length; i++)
            {
                services.AddSingleton(serviceType:baseType, implementationType: composerTypes[i]);
            }

            return services;
        }
    }
}