namespace Dariosoft.gRPCTool.ProtoWriters
{
    public class ProtoHeaderWriter : ProtoWriter
    {
        public ProtoHeaderWriter( ProtoServiceWriter next)
            : base(next)
        {
        }

        protected override void Process(Utilities.IOuputWriter writer, Components.ProtobufComponent component)
        {
            writer.WriteLine($"syntax = \"{component.ProtobufSyntax}\";");
            writer.WriteLine($"package {component.ProtobufPackage};");
            if (!string.IsNullOrWhiteSpace(component.CSharpNameSpace))
                writer.WriteLine($"option csharp_namespace = \"{component.CSharpNameSpace}\";");
            writer.WriteLine();

            WriteImports(writer, component.Imports);
        }

        private void WriteImports(Utilities.IOuputWriter writer, IReadOnlyCollection<string> imports)
        {
            if (imports.Count < 1) return;
            writer.WriteLine("//--------------- Imports ---------------");
            for (var i = 0; i < imports.Count; i++)
                writer.WriteLine($"import \"{imports.ElementAt(i)}\";");
            writer.WriteLine();
        }
    }
}