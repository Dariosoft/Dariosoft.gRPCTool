namespace Dariosoft.gRPCTool.V2.Composers
{
    public class ProtobufMessageComponentComposer() : ComponentComposer(null)
    {
        protected override void Process(Components.ProtobufComponent component)
        {
            //TODO: Generate message and push to ProtobufModel.Messages
            //1: Something like a recursive function must resolve all the complex messages and reply messages
            //2: start from request message parameters type
            //3: Then reply message parameter type
        }
    }
}