namespace Dariosoft.gRPCTool.ProtoWriters
{
    public class ProtoServiceWriter : ProtoWriter
    {
        public ProtoServiceWriter(ProtoEnumWriter next)
            : base(next)
        {
        }

        protected override void Process(Utilities.IOuputWriter writer, Components.ProtobufComponent component)
        {
            if (component.Services.Count < 1) return;
            writer.WriteLine("//--------------- Services ---------------");
            for (var i = 0; i < component.Services.Count; i++)
            {
                WriteAService(writer, component.Services.ElementAt(i));
                writer.WriteLine();
            }
        }

        private void WriteAService(Utilities.IOuputWriter writer, Components.ProtobufServiceComponent service)
        {
            writer.WriteLine($"service {service.Name.ProtobufName} {{");
            for (var i = 0; i < service.Procedures.Count; i++)
                WriteAProcedure(writer, service.Procedures.ElementAt(i));

            writer.WriteLine("}");
        }

        private void WriteAProcedure(Utilities.IOuputWriter writer, Components.ProtobufProcedureComponent procedure)
        {
            var reqMessage = procedure.RequestMessage is null ? Utilities.GoogleProtobuf.EmptyMessage : procedure.RequestMessage.Name.ProtobufName;
            var repMessage = procedure.ReplyMessage is null ? Utilities.GoogleProtobuf.EmptyMessage : procedure.ReplyMessage.Name.ProtobufName;
            
            writer.WriteLine($"rpc {procedure.Name.ProtobufName}({reqMessage}) returns({repMessage});");
        }
    }
}