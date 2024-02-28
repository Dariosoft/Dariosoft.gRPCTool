namespace Dariosoft.gRPCTool.V2.Utilities
{
    class ConsoleLogger : ILogger
    {
        public void Info(string message)
        {
            Console.WriteLine("--- BEGIN Info[{0}] -----------------------------------------", DateTime.Now.ToString("HH:mm dd-MMM"));
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("--- END Info -------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        public void Warning(string message)
        {
            Console.WriteLine("--- BEGIN Warning([{0}] -----------------------------------------", DateTime.Now.ToString("HH:mm dd-MMM"));
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(message);
            Console.ResetColor();
            Console.WriteLine("--- END Warning -------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }

        public void LogError(string where, Exception exception, string description = "")
        {
            Console.WriteLine("--- BEGIN ERROR[{0}] -----------------------------------------", DateTime.Now.ToString("HH:mm dd-MMM"));
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error: {0}, {1}({2})", where, exception.GetType().Name, exception.Message);
            if (!string.IsNullOrWhiteSpace(description))
                Console.WriteLine(description);
            Console.WriteLine("--- Stack Trace -----");
            Console.WriteLine(exception.StackTrace);
            Console.ResetColor();
            Console.WriteLine("--- END ERROR -------------------------------------------");
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
