using System.Reflection;

namespace Dariosoft.gRPCTool.ElementTypes
{
    public abstract record Element<TSource>
        where TSource : MemberInfo
    {
        protected Element(TSource source, Enums.ElementType elementType)
        {
            ValidateServiceElement(source, elementType);
            ValidateProcedureElement(source, elementType);
            ValidateEnumElement(source, elementType);
            ValidateMessageElement(source, elementType);
            this.Source = source;
            this.Type = elementType;
        }
        
        public Enums.ElementType Type { get; }
        
        public TSource Source { get; }
        
        public override int GetHashCode() => Source.GetHashCode();

        #region Validate

        private void ValidateServiceElement(TSource source, Enums.ElementType elementType)
        {
            if (elementType != Enums.ElementType.Service) return;

            if (source is not System.Type) 
                ThrowTypeMatchException(elementType);

            var t = (source as Type)!;
            
            if(t.IsEnum || t.IsPrimitive || !(t.IsClass || t.IsInterface))
                ThrowTypeMatchException(elementType);
        }
        private void ValidateProcedureElement(TSource source, Enums.ElementType elementType)
        {
            if (elementType != Enums.ElementType.Procedure) return;

            if (source is not MethodInfo) 
                ThrowTypeMatchException(elementType);
        }
        private void ValidateEnumElement(TSource source, Enums.ElementType elementType)
        {
            if (elementType != Enums.ElementType.Enum) return;

            if (source is not System.Type) 
                ThrowTypeMatchException(elementType);

            var t = (source as Type)!;
            
            if(!t.IsEnum)
                ThrowTypeMatchException(elementType);
        }
        private void ValidateMessageElement(TSource source, Enums.ElementType elementType)
        {
            if (elementType != Enums.ElementType.Message) return;

            if (source is not System.Type) 
                ThrowTypeMatchException(elementType);

            var t = (source as Type)!;
        }
        private void ThrowTypeMatchException(Enums.ElementType elementType)
        {
            throw new ArgumentException($"The source does not match to the element type of \"{Enum.GetName(elementType)}\".");
        }

        #endregion
    }
}