using Dariosoft.gRPCTool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IFactories = Dariosoft.gRPCTool.Factories;
using IFilters = Dariosoft.gRPCTool.Filters;

namespace iosoft.gRPCTool.Console;

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
            .AddSingleton<IFilters.IServiceTypeFilter, Filters.ServiceTypeFilter>()
            .AddSingleton<IFactories.IOutputWriterFactory, Factories.OutputWriterFactory>()
         //   .AddSingleton<TypeRefineries.ITypeRefiner, RequestRefiner>()
            .AddgRPCTool(options =>
            {
                options.NugetPackagesDirectory = builder.Configuration.GetNugetPackagesDirectory();
                options.AssemblySearchPaths = builder.Configuration.GetAssemblySearchPath();
               // options.AssemblyFiles = builder.Configuration.GetAssemblyFiles();
            })
            .AddSingleton<Utilities.AssemblyRepository>()
            .AddSingleton<Utilities.Menu>()
            .Configure<Utilities.AssemblyRepositoryOptions>(options =>
            {
                options.Items.Add(new Utilities.AssemblyRepositoryItem("MicroZaren.Platform.Location.Abstraction.dll", @"D:\Zaren\Projects\micro-zaren-platform-location\MicroZaren.Platform.Location.Abstraction\bin\Release\net8.0"));
                options.Items.Add(new Utilities.AssemblyRepositoryItem("Dariosoft.AppCenter.EndPoint.Abstraction.dll", @"D:\Dariosoft\Projects\2024\AppCenter_V2\Backend\Dariosoft.AppCenter\Apps\EndPoint.Abstraction\bin\Release\net8.0"));
            });

        var host = builder.Build();

        host.Services.GetService<Utilities.Menu>()!.Show();

        
    }
}