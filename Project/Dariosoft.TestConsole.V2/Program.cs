using System.Globalization;
using Dariosoft.gRPCTool.V2;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Factories = Dariosoft.gRPCTool.V2.Factories;
using Filters = Dariosoft.gRPCTool.V2.Filters;
using TypeRefineries = Dariosoft.gRPCTool.V2.TypeRefineries;

namespace Dariosoft.TestConsole.V2;

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

class RequestRefiner : TypeRefineries.ITypeRefiner
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
            .AddSingleton<Filters.IServiceTypeFilter, ServiceTypeFilter>()
            .AddSingleton<Factories.IOutputWriterFactory, OutputWriterFactory>()
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
        
/*
        var refinery = host.Services.GetService<TypeRefineries.ITypeRefinery>()!;

        Type reqType1 = typeof(IRequest<int?>)
            , reqType2 = typeof(IRequest)
            , respType = typeof(Task<IResponse<gRPCTool.V2.Models.NameModel?>>);

        var refinedReqType1 = refinery.Refine(reqType1, out var reqSummary1);
        var refinedReqType2 = refinery.Refine(reqType2, out var reqSummary2);
        var refinedRespType = refinery.Refine(respType, out var respSummary);
*/
        var engine = host.Services.GetService<IEngine>()!;

        engine.Start();
    }
}