namespace Dariosoft.gRPCTool.ProtobufWriters
{
    public interface IProtobufHeaderWriter
    {
        void Write(Accessories.ITextWriter output, Models.ProtobufHeader value);
    }

    internal class ProtobufHeaderWriter : IProtobufHeaderWriter
    {
        public void Write(Accessories.ITextWriter output, Models.ProtobufHeader value)
        {
            output.WriteLine($"syntax = \"{value.ProtobufSyntax}\";");
            output.WriteLine($"package {value.ProtobufPackage};");
            if (!string.IsNullOrWhiteSpace(value.CSharpNameSpace))
                output.WriteLine($"option csharp_namespace = \"{value.CSharpNameSpace}\";");
            output.WriteLine();

            if (value.Imports.Length > 0)
            {
                output.WriteLine("/*---------- Imports ----------*/");
                for (int i = 0; i < value.Imports.Length; i++)
                {
                    output.WriteLine($"import \"{value.Imports[i]}\";");
                }

                output.WriteLine();
            }
        }
    }
}
