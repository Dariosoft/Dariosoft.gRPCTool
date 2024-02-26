namespace Dariosoft.gRPCTool.ProtobufWriters
{
    public interface IProtobufServiceWriter
    {
        void Write(Accessories.ITextWriter output, IReadOnlyList<Models.ProtobufService> services);
    }

    internal class ProtobufServiceWriter : IProtobufServiceWriter
    {
        public void Write(Accessories.ITextWriter output, IReadOnlyList<Models.ProtobufService> services)
        {
            if (services.Count < 1) return;

            output.WriteLine("/*---------- Services ----------*/");

            for (var i = 0; i < services.Count; i++)
                Write(output, services[i]);
        }

        private void Write(Accessories.ITextWriter output, Models.ProtobufService service)
        {
            output.WriteLine($"service {service.Name.ProtobufName} {{");

            for (var i = 0; i < service.Procedures.Count; i++)
                output.WriteLine($"\trpc {service.Procedures[i].Name.ProtobufName}({service.Procedures[i].RequestMessage.Name.ProtobufName}) returns ({service.Procedures[i].ResponseMessage.Name.ProtobufName});");

            output.WriteLine("}");
            output.WriteLine();
        }
    }
}
