
namespace Dariosoft.gRPCTool.Factories
{
    public interface IProtoFileFactory
    {
        Utilities.IOuputWriter Create(Components.ProtobufComponent component);
    }
    
    class ProtoFileFactory(IOutputWriterFactory outputWriterFactory, ProtoWriters.ProtoHeaderWriter writer):IProtoFileFactory
    {
        public  Utilities.IOuputWriter Create(Components.ProtobufComponent component)
        {
            var protoFileOutput = outputWriterFactory.CreateProtoFileOutputWriter(component);
            writer.Accept(protoFileOutput, component);
            return protoFileOutput;
        }
        
    }
}