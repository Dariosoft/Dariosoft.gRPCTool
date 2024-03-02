namespace Dariosoft.gRPCTool.Factories
{
    public interface IOutputWriterFactory
    {
        Utilities.IOuputWriter CreateProtoFileOutputWriter(Components.ProtobufComponent component);
    }
}