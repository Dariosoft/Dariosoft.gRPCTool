namespace Dariosoft.gRPCTool.Enums
{
    public enum ElementType : byte
    {
        Service = 1,
        Procedure = 2,
        Message = 3,
        //ValueMessage = 4,
        Enum = 5,
        RpcRequestMessage = 6,
        RpcReplyMessage = 7
    }
}