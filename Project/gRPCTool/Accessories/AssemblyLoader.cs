using Microsoft.Extensions.Options;
using System.Reflection;
using System.Runtime.Loader;

namespace Dariosoft.gRPCTool.Accessories
{
    public interface IAssemblyLoader
    {
        Assembly? Load(string filename);
    }

    internal class AssemblyLoader : IAssemblyLoader
    {
        private readonly Options _options;
        private readonly ILogger _logger;
        public AssemblyLoader(ILogger logger, IOptions<Options> options)
        {
            _logger = logger;
            _options = options.Value;
            AssemblyLoadContext.Default.Resolving += OnAssemblyResolve;
        }

        public Assembly? Load(string filename)
        {
            try
            {
                return File.Exists(filename)
                        ? AssemblyLoadContext.Default.LoadFromAssemblyPath(filename)
                        : null;
            }
            catch (Exception err)
            {
                _logger.LogError("AssemblyLoader.Load()", err);
                return null;
            }
        }
        private Assembly? OnAssemblyResolve(AssemblyLoadContext context, AssemblyName assemblyName)
        {
            var assembly = ResolveAssembly(context, assemblyName, [_options.NugetPackagesDirectory]);
            if (assembly is null)
                assembly = ResolveAssembly(context, assemblyName, _options.AssemblySearchPaths);
            return assembly;
        }

        private Assembly? ResolveAssembly(AssemblyLoadContext context, AssemblyName assemblyName, string[] searchDirectories)
        {
            var matchedFiles = searchDirectories
                .Distinct()
                .SelectMany(path => Directory.GetFiles(path, $"{assemblyName.Name}.dll", SearchOption.AllDirectories))
                .ToArray();

            Assembly? target = null;
            AssemblyName asmName;

            try
            {
                for (var i = 0; i < matchedFiles.Length; i++)
                {
                    asmName = AssemblyName.GetAssemblyName(matchedFiles[i]);

                    if (asmName.FullName == assemblyName.FullName)
                    {
                        target = context.LoadFromAssemblyPath(matchedFiles[i]);
                        break;
                    }
                }
            }
            catch (Exception err)
            {
                _logger.LogError("AssemblyLoader.OnAssemblyResolve()", err);
            }


            return target;
        }


    }
}
