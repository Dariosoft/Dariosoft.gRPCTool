namespace Dariosoft.gRPCTool.Utilities
{
    public sealed class GoogleProtobuf
    {
        public static readonly string[] Protobufs =
        [
            "google/protobuf/empty.proto",
            "google/protobuf/any.proto",
            "google/protobuf/timestamp.proto",
            "google/protobuf/duration.proto",
            "google/protobuf/wrappers.proto",
        ];

        public const string EmptyMessage = "google.protobuf.Empty";
        public const string AnyMessage = "google.protobuf.Any";

        public const string Timestamp = "google.protobuf.Timestamp";
        public const string Duration = "google.protobuf.Duration";
        public const string Bytes = "bytes";

        public static readonly IDictionary<Type, string> TypeMap = new Dictionary<Type, string>
        {
            { typeof(bool), "bool" },
            { typeof(double), "double" },
            { typeof(float), "float" },
            { typeof(int), "int32" },
            { typeof(uint), "uint32" },
            { typeof(long), "int64" },
            { typeof(ulong), "uint64" },
            { typeof(string), "string" },
            // { typeof(Guid), "string"},
            { typeof(byte[]), GoogleProtobuf.Bytes },
            // { typeof(TimeOnly), GoogleProtobuf.Duration},
            { typeof(TimeSpan), GoogleProtobuf.Duration },
            { typeof(DateOnly), GoogleProtobuf.Timestamp },
            { typeof(DateTime), GoogleProtobuf.Timestamp },
            { typeof(DateTimeOffset), GoogleProtobuf.Timestamp },
        };

        public static readonly IDictionary<Type, string> NullableTypeMap = new Dictionary<Type, string>
        {
            { typeof(bool), "google.protobuf.BoolValue" },
            { typeof(double), "google.protobuf.DoubleValue" },
            { typeof(float), "google.protobuf.FloatValue" },
            { typeof(int), "google.protobuf.Int32Value" },
            { typeof(uint), "google.protobuf.UInt32Value" },
            { typeof(long), "google.protobuf.Int64Value" },
            { typeof(ulong), "google.protobuf.UInt64Value" },
        };
    }
}