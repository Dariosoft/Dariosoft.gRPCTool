﻿namespace Dariosoft.gRPCTool.Accessories
{
    public interface ILogger
    {
        void Info(string message);
        void Warning(string message);
        void LogError(string where, Exception exception, string description = "");
    }
}
