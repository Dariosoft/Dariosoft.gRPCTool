namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IOutputWriterFactory
    {
        Utilities.IOuputWriter Create(Components.ProtobufComponent component);
    }
}