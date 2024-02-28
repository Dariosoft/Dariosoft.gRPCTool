namespace Dariosoft.gRPCTool.V2.Composers
{
    public abstract class ComponentComposer
    {
        protected ComponentComposer(ComponentComposer? next)
        {
            this.Next = next;
        }

        protected ComponentComposer? Next { get; }

        protected virtual void Process(Components.ProtobufComponent component)
        {
        }

        public void Accept(Components.ProtobufComponent component)
        {
            Process(component);
            Next?.Accept(component);
        }
    }
}