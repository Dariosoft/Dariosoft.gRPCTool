namespace Dariosoft.gRPCTool.Factories
{
    public interface IOutputWriterFactory
    {
        Utilities.IOuputWriter Create(Components.ProtobufComponent component);
    }
}