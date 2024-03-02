namespace Dariosoft.gRPCTool.ProtoWriters
{
    public class ProtoEnumWriter : ProtoWriter
    {
        public ProtoEnumWriter(ProtoMessageWriter next)
            : base(next)
        {
        }

        protected override void Process(Utilities.IOuputWriter writer, Components.ProtobufComponent component)
        {
            if (component.Enums.Count < 1) return;
            writer.WriteLine("//--------------- Enums ---------------");
            for (var i = 0; i < component.Enums.Count; i++)
            {
                WriteAnEnum(writer, component.Enums.ElementAt(i));
                writer.WriteLine();
            }
        }

        private void WriteAnEnum(Utilities.IOuputWriter writer, Components.ProtobufEnumComponent @enum)
        {
            writer.WriteLine($"enum {@enum.Name.ProtobufName} {{");
            var keys = @enum.Members.Keys.Order().ToArray();

            for (var i = 0; i < keys.Length; i++)
                writer.WriteLine($"{@enum.Members[keys[i]]} = {keys[i]};");

            writer.WriteLine("}");
        }
    }
}