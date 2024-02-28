using System.Reflection;

namespace Dariosoft.gRPCTool.V2.Elements
{
    public abstract class Element
    {
        protected Element(MemberInfo target, Enums.ElementType type)
        {
            this.Target = target;
            this.Type = type;
        }

        public MemberInfo Target { get; }

        public Enums.ElementType Type { get; }
    }

    public class ServiceElement : Element
    {
        public ServiceElement(Type serviceType)
            : base(Elligable(serviceType) ? serviceType : throw new ArgumentException("The service type must be a class or an interface.", nameof(serviceType)), Enums.ElementType.Service)
        {
            this.ServiceType = serviceType;
        }

        public Type ServiceType { get; }

        private static bool Elligable(Type serviceType)
        {
            return (serviceType.IsClass || serviceType.IsInterface) &&
                   serviceType != typeof(object) &&
                   serviceType != typeof(void) &&
                   serviceType is { IsEnum: false, IsPrimitive: false };
        }
    }

    public class ProcedureElement : Element
    {
        public ProcedureElement(MethodInfo methodInfo)
            : base(methodInfo, Enums.ElementType.Procedure)
        {
            this.MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }
    }

    public class RequestMessageElement : Element
    {
        public RequestMessageElement(MethodInfo methodInfo)
            : base(methodInfo, Enums.ElementType.RequestMessage)
        {
            this.MethodInfo = methodInfo;
        }

        public MethodInfo MethodInfo { get; }

        public bool HasParameter() => MethodInfo.GetParameters().Any(p => !p.IsOut);
    }

    /* public class ReplyMessageElement : Element
     {
         public ReplyMessageElement(Type returnType)
             : base(returnType, Enums.ElementType.ReplyMessage)
         {
         }
     }*/

    public class MessageElement : Element
    {
        private MessageElement(Models.XType target, Enums.ElementType type)
            : base(target.Type, type)
        {
            MessageType = target;
        }

        public Models.XType MessageType { get; }
        public static MessageElement ReplyMessage(Models.XType target) => new MessageElement(target, Enums.ElementType.ReplyMessage);

        public static MessageElement DataMessage(Models.XType target) => new MessageElement(target, Enums.ElementType.Message);
    }

    public class EnumElement : Element
    {
        public EnumElement(Type type)
            : base(type.IsEnum ? type : throw new ArgumentException("The type is not an enum.", nameof(type)), Enums.ElementType.Enum)
        {
        }
    }
}