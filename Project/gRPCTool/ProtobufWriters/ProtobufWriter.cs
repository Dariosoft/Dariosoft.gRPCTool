namespace Dariosoft.gRPCTool.ProtobufWriters
{
    public interface IProtobufWriter
    {
        void Write(Accessories.ITextWriter output, Models.Protobuf value);
    }

    internal class ProtobufWriter(
        IProtobufHeaderWriter headerWriter,
        IProtobufServiceWriter serviceWriter,
        IProtobufEnumMessageWriter enumMessageWriter,
        IProtobufMessageWriter messageWriter
        ) : IProtobufWriter
    {

        public void Write(Accessories.ITextWriter output, Models.Protobuf value)
        {
            headerWriter.Write(output, value.Header);
            serviceWriter.Write(output, value.Services);
            messageWriter.Write(output, value.Messages);
            enumMessageWriter.Write(output, value.Enums);
        }
    }
}
