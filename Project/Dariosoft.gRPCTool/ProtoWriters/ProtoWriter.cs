namespace Dariosoft.gRPCTool.ProtoWriters
{
    public abstract class ProtoWriter
    {
        protected ProtoWriter( ProtoWriter? next)
        {
            this.Next = next;
        }
      
        protected ProtoWriter? Next { get; }

        protected abstract void Process(Utilities.IOuputWriter writer, Components.ProtobufComponent component);

        public void Accept(Utilities.IOuputWriter writer, Components.ProtobufComponent component)
        {
            Process(writer, component);
            Next?.Accept(writer, component);
        }
    }
}