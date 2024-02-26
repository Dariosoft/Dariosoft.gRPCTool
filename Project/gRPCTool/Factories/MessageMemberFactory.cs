using System.Reflection;

namespace Dariosoft.gRPCTool.Factories
{
    public interface IMessageMemberFactory
    {
        Models.MessageMember Create(int memberIndex, ParameterInfo parameter);

        Models.MessageMember Create(int memberIndex, PropertyInfo property);
    }

    internal class MessageMemberFactory(
        Providers.IProtobufTypeProvider protobufTypeProvider,
        Analyzers.ITypeAnalyzer typeAnalyzer) : IMessageMemberFactory
    {
        public Models.MessageMember Create(int memberIndex, ParameterInfo parameter)
        {
            var type = protobufTypeProvider.Provide(typeAnalyzer.Analyze(parameter.ParameterType));

            return new Models.MessageMember
            {
                Index = memberIndex,
                Name = parameter.Name!.ToPublicCase(),
                DataType = type.TypeName,
                OneOf = type.OneOf ? [new Models.MessageMemberOneOf { DataType = type.TypeName, Index = 1, Name = parameter.Name!.ToPrivateCase() }] : []
            };
        }

        public Models.MessageMember Create(int memberIndex, PropertyInfo property)
        {
            var type = protobufTypeProvider.Provide(typeAnalyzer.Analyze(property.PropertyType));

            return new Models.MessageMember
            {
                Index = memberIndex,
                Name = property.Name.ToPublicCase(),
                DataType = type.TypeName,
                OneOf = type.OneOf ? [new Models.MessageMemberOneOf { DataType = type.TypeName, Index = 1, Name = property.Name.ToPrivateCase() }] : []
            };
        }
    }
}
