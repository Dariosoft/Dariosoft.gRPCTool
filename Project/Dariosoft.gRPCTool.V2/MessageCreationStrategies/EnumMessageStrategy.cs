namespace Dariosoft.gRPCTool.V2.MessageCreationStrategies
{
    class EnumMessageStrategy(Factories.INameFactory nameFactory) : MessageCreationStrategy
    {
        public Components.ProtobufEnumComponent Create(Elements.EnumElement element)
        {
            var name = nameFactory.Create(element);
            
            var component = new Components.ProtobufEnumComponent
            {
                Name = name,
                Members = Utilities.EnumHelper.Instance.GetItems(element.MessageType, name.Name)
            };

           
            if(!component.Members.ContainsKey(0))
                component.Members.Add(0, $"{component.Name.Name}__Unknown");
            
            return component;
        }
    }
}