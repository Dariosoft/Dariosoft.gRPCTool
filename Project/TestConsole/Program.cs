using Dariosoft.gRPCTool;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Dariosoft.TestConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            args =
            [
                // "--nugets-path=bugetfolder",
                "--assembly-search-path=D:/Dariosoft/Projects/2024/AppCenter_V2/Backend/Dariosoft.AppCenter",
                "--assembly=D:/Dariosoft/Projects/2024/AppCenter_V2/Backend/Dariosoft.AppCenter/Apps/EndPoint.Abstraction/bin/Debug/net8.0/Dariosoft.AppCenter.EndPoint.Abstraction.dll",
            ];

            var builder = Host.CreateApplicationBuilder(args);

            builder.Configuration
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddCommandLine(args);


            builder.Services
                .AddSingleton<gRPCTool.Filters.IServiceTypeFilter, ServiceTypeFilter>()
                .AddgRPCTool(options =>
                {
                    options.NugetPackagesDirectory = builder.Configuration.GetNugetPackagesDirectory();
                    options.AssemblySearchPaths = builder.Configuration.GetAssemblySearchPath();
                    options.AssemblyFiles = builder.Configuration.GetAssemblyFiles();
                });

            var host = builder.Build();


            using (var file = new StreamWriter(@"D:\Dariosoft\Projects\2024\Dariosoft.gRPCTool\Project\TestConsole\Protos\app_center.proto"))
            {
                var output = new MultiTextWriter(Console.Out, file);

                var engine = host.Services.GetService<IEngine>()!;

                engine.Start(output);
            }

            // host.Run();

            Console.WriteLine("The application has been terminated.");
        }
    }
}