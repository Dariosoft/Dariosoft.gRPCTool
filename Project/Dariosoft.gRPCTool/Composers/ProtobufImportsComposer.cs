namespace Dariosoft.gRPCTool.Composers
{
    public class ProtobufImportsComposer(ProtobufServiceComponentComposer next) : ComponentComposer(next)
    {
        protected override void Process(Components.ProtobufComponent component)
        {
            for (var i = 0; i < Utilities.GoogleProtobuf.Protobufs.Length; i++)
            {
                component.Imports.Add(Utilities.GoogleProtobuf.Protobufs[i]);
            }
        }
    }
}