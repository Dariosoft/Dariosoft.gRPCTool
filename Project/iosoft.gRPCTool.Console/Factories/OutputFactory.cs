using Dariosoft.gRPCTool.Components;
using iosoft.gRPCTool.Console.Utilities;
using IOuputWriter = Dariosoft.gRPCTool.Utilities.IOuputWriter;
using IOutputWriterFactory = Dariosoft.gRPCTool.Factories.IOutputWriterFactory;

namespace iosoft.gRPCTool.Console.Factories
{
    public class OutputWriterFactory : IOutputWriterFactory
    {
        public IOuputWriter CreateProtoFileOutputWriter(ProtobufComponent component)
        {
            var asmbelyName = component.Source.GetName();
            
            switch (asmbelyName.Name)
            {
                case "MicroZaren.Platform.Location.Abstraction": return GetZarenLocation();
                case "Dariosoft.AppCenter.EndPoint.Abstraction": return GetDariosoftAppCenter();
                default:
                    return new MultiTextWriter(System.Console.Out);
            }
        }

        private IOuputWriter GetZarenLocation()
        {
            var filename = @"D:\Dariosoft\Projects\Samples\Dariosoft.gRPCClientSDK\Zaren.Platforms.LocationService.gRPCClientSDK\Protos\location_service.proto";
            return new MultiTextWriter(System.Console.Out, new StreamWriter(filename));
        }

        private IOuputWriter GetDariosoftAppCenter()
        {
            var filename = @"D:\Dariosoft\Projects\Samples\Dariosoft.gRPCClientSDK\Dariosoft.AppCenter.gRPCClientSDK\Protos\appcenter_service.proto";
            return new MultiTextWriter(System.Console.Out, new StreamWriter(filename));
        }
    }
}