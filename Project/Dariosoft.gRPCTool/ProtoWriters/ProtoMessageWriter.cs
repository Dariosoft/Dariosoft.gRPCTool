namespace Dariosoft.gRPCTool.ProtoWriters
{
    public class ProtoMessageWriter : ProtoWriter
    {
        public ProtoMessageWriter()
            : base(null)
        {
        }

        protected override void Process(Utilities.IOuputWriter writer, Components.ProtobufComponent component)
        {
            if (component.Messages.Count < 1) return;

            writer.WriteLine("//--------------- Messages ---------------");
            for (var i = 0; i < component.Messages.Count; i++)
            {
                WriteAMessage(writer, component.Messages.ElementAt(i));
                writer.WriteLine();
            }
        }

        private void WriteAMessage(Utilities.IOuputWriter writer, Components.ProtobufMessageComponent message)
        {
            var members = message.Members.OrderBy(m => m.Index);
            var count = members.Count();

            writer.WriteLine($"message {message.Name.ProtobufName} {{");
            for (var i = 0; i < count; i++)
                WriteAMessageMember(writer, members.ElementAt(i));

            writer.WriteLine("}");
        }

        private void WriteAMessageMember(Utilities.IOuputWriter writer, Components.ProtobufMessageMemberComponent member)
        {
            if (member.OneOf.Length > 0)
                WriteOneof(member.Name.ProtobufName, writer, member.OneOf);
            else
                writer.WriteLine($"{member.DataType} {member.Name} = {member.Index};");
        }

        private void WriteOneof(string name, Utilities.IOuputWriter writer, Components.MessageMemberOneOf[] items)
        {
            writer.WriteLine($"oneof {name} {{");
            items = items.OrderBy(i => i.Index).ToArray();
            for (var i = 0; i < items.Length; i++)
                writer.WriteLine($"{items[i].DataType} {items[i].Name} = {items[i].Index};");
            writer.WriteLine("}");
        }
    }
}