
namespace Dariosoft.gRPCTool.V2.Factories
{
    public interface IProtoFileFactory
    {
        void Create(Components.ProtobufComponent component);
    }
    
    class ProtoFileFactory(IOutputWriterFactory outputWriterFactory, ProtoWriters.ProtoHeaderWriter writer):IProtoFileFactory
    {
        public void Create(Components.ProtobufComponent component)
        {
            var protoFileOutput = outputWriterFactory.Create(component);
            writer.Accept(protoFileOutput, component);
            protoFileOutput.Flush();
        }
        
    }
}