using System.Globalization;
using Dariosoft.gRPCTool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Factories = Dariosoft.gRPCTool.Factories;
using Filters = Dariosoft.gRPCTool.Filters;
using IOutputWriterFactory = Dariosoft.gRPCTool.Factories.IOutputWriterFactory;
using IServiceTypeFilter = Dariosoft.gRPCTool.Filters.IServiceTypeFilter;
using ITypeRefiner = Dariosoft.gRPCTool.TypeRefineries.ITypeRefiner;
using TypeRefineries = Dariosoft.gRPCTool.TypeRefineries;

namespace Dariosoft.TestConsole;

public interface IRequest
{
}

public interface IRequest<out T> : IRequest
{
    T Payload { get; }
}

public interface IResponse<out T>
{
    T Data { get; }
}

class RequestRefiner : ITypeRefiner
{
    public bool Enabled { get; } = true;
    public int Order { get; } = 1;

    public Type Refine(Type input)
    {
        if(input.IsAssignableTo(typeof(Dariosoft.Framework.IRequest)))
        {
            input = input.GenericTypeArguments.Length > 0 
                ? input.GenericTypeArguments[0] 
                : typeof(void);
        }
        
        return input;
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        
        args =
        [
            // "--nugets-path=nugetfolder",
            "--assembly-search-path=D:/Dariosoft/Projects/2024/AppCenter_V2/Backend/Dariosoft.AppCenter",
            "--assembly=D:/Dariosoft/Projects/2024/AppCenter_V2/Backend/Dariosoft.AppCenter/Apps/EndPoint.Abstraction/bin/Debug/net8.0/Dariosoft.AppCenter.EndPoint.Abstraction.dll",
        ];

        var builder = Host.CreateApplicationBuilder(args);

        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddCommandLine(args);


        builder.Services
            .AddSingleton<IServiceTypeFilter, ServiceTypeFilter>()
            .AddSingleton<IOutputWriterFactory, OutputWriterFactory>()
         //   .AddSingleton<TypeRefineries.ITypeRefiner, RequestRefiner>()
            .AddgRPCTool(options =>
            {
                options.NugetPackagesDirectory = builder.Configuration.GetNugetPackagesDirectory();
                options.AssemblySearchPaths = builder.Configuration.GetAssemblySearchPath();
                options.AssemblyFiles = builder.Configuration.GetAssemblyFiles();
            });

        var host = builder.Build();
        
      //  var xTypeFactory = host.Services.GetService<Factories.IXTypeFactory>()!;

       // var xInfo = xTypeFactory.Create(typeof(IRequest<ValueTask<int?>>));
        

        var engine = host.Services.GetService<IEngine>()!;

        engine.Start();
    }
}