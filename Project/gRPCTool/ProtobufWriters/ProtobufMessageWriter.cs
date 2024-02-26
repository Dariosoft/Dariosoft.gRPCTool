using System.Text;

namespace Dariosoft.gRPCTool.ProtobufWriters
{
    public interface IProtobufMessageWriter
    {
        void Write(Accessories.ITextWriter output, IReadOnlyList<Models.ComplexMessage> messages);
    }

    internal class ProtobufMessageWriter : IProtobufMessageWriter
    {
        public void Write(Accessories.ITextWriter output, IReadOnlyList<Models.ComplexMessage> messages)
        {
            if (messages.Count < 1) return;

            output.WriteLine("/*---------- Messages ----------*/");

            for (var i = 0; i < messages.Count; i++)
                Write(output, messages[i]);
        }

        private void Write(Accessories.ITextWriter output, Models.ComplexMessage message)
        {
            output.WriteLine($"message {message.Name.ProtobufName} {{");

            for (var i = 0; i < message.Members.Count; i++)
            {
                if (message.Members[i].OneOf.Length > 0)
                    output.WriteLine(GetOneOfMemeber(message.Members[i]));
                else
                    output.WriteLine(GetSimpleMemeber(message.Members[i]));
            }

            output.WriteLine("}");
            output.WriteLine();
        }

        private string GetOneOfMemeber(Models.MessageMember member)
        {
            if (member.OneOf.Length == 1)
                return $"\toneof {member.Name} {{ {member.OneOf[0].DataType} {member.OneOf[0].Name} = {member.OneOf[0].Index}; }}";
            else
            {
                var lines = new StringBuilder();
                try
                {
                    lines.AppendLine($"\toneof {member.Name} {{");
                    for (var i = 0; i < member.OneOf.Length; i++)
                        lines.AppendLine($"\t{member.OneOf[i].DataType} {member.OneOf[i].Name} = {member.OneOf[i].Index}");
                    lines.AppendLine("}");
                    return lines.ToString();
                }
                finally
                {
                    lines.Clear();
                }

            }
        }

        private string GetSimpleMemeber(Models.MessageMember member)
           => $"\t{member.DataType} {member.Name} = {member.Index};";
    }
}
