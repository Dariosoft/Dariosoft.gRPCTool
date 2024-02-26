namespace Dariosoft.gRPCTool.ProtobufWriters
{
    public interface IProtobufEnumMessageWriter
    {
        void Write(Accessories.ITextWriter output, IReadOnlyList<Models.EnumMessage> messages);
    }

    internal class ProtobufEnumMessageWriter: IProtobufEnumMessageWriter
    {
        public void Write(Accessories.ITextWriter output, IReadOnlyList<Models.EnumMessage> messages)
        {
            if (messages.Count < 1) return;

            output.WriteLine("/*---------- Enums ----------*/");

            for (var i = 0; i < messages.Count; i++)
                Write(output, messages[i]);
        }

        private void Write(Accessories.ITextWriter output, Models.EnumMessage message)
        {
            output.WriteLine($"enum {message.Name.ProtobufName} {{");
            
            for (var i = 0; i < message.Items.Count; i++)
                output.WriteLine($"\t{message.Items.ElementAt(i).Key} = {message.Items.ElementAt(i).Value};");

            output.WriteLine("}");
            output.WriteLine();
        }
    }
}
