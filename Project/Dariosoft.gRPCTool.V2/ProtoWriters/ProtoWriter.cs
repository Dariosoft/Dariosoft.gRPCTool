namespace Dariosoft.gRPCTool.V2.ProtoWriters
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
            writer.WriteLine($"rpc {procedure.Name.ProtobufName}({procedure.RequestMessage.Name.ProtobufName}) returns({procedure.ReplyMessage.Name.ProtobufName});");
        }
    }

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
            writer.WriteLine($"{name} {{");
            items = items.OrderBy(i => i.Index).ToArray();
            for (var i = 0; i < items.Length; i++)
                writer.WriteLine($"{items[i].DataType} {items[i].Name} = {items[i].Index};");
            writer.WriteLine("}");
        }
    }
    

}