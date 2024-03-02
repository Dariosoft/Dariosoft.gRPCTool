namespace Dariosoft.gRPCTool.Models
{
    public record NameModel
    {
        #region Constructors

        public NameModel(string name, string protobufName)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));
            
            if (string.IsNullOrWhiteSpace(protobufName))
                throw new ArgumentNullException(nameof(protobufName));
            
            this.Name = name;
            this.ProtobufName = protobufName;
        }

        public NameModel(string name)
            : this(name, name)
        {
        }

        #endregion

        public string Name { get; }

        public string ProtobufName { get; }

        public override string ToString() => Name;
    };
}