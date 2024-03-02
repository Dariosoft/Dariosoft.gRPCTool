using System.Reflection;

namespace Dariosoft.gRPCTool.Elements
{
    public abstract class Element
    {
        protected Element(MemberInfo target, Enums.ElementType type)
        {
            if (type is Enums.ElementType.Procedure or Enums.ElementType.RequestMessage && target is not MethodInfo)
                throw new ArgumentException("Mismatch target.", nameof(target));

            if (type is not (Enums.ElementType.Procedure or Enums.ElementType.RequestMessage) && target is MethodInfo)
                throw new ArgumentException("Mismatch target.", nameof(target));

            if (type is not Enums.ElementType.Enum && target is System.Type { IsEnum: true })
                throw new ArgumentException("Mismatch target.", nameof(target));

            this.Target = target;
            this.Type = type;
            Key = GenearetKey();
        }

        public string Key { get; }
        public MemberInfo Target { get; }

        public Enums.ElementType Type { get; }

        private string GenearetKey()
        {
            var name = Target switch
            {
                Type t => (t.FullName ?? t.Name).ComputeHash(),
                MethodInfo m => $"{m.DeclaringType!.FullName ?? m.DeclaringType.Name}_{m.Name}".ComputeHash(),
                _ => Type switch
                {
                    Enums.ElementType.EmptyMessage => Utilities.GoogleProtobuf.EmptyMessage,
                    _ => ""
                }
            };

            return name;
        }

        public override int GetHashCode() => Key.GetHashCode();
    }

    /* public class ReplyMessageElement : Element
     {
         public ReplyMessageElement(Type returnType)
             : base(returnType, Enums.ElementType.ReplyMessage)
         {
         }
     }*/
}