namespace Dariosoft.gRPCTool.V2.Utilities
{
    public interface IOuput : IDisposable, IAsyncDisposable
    {
        #region Write
        void Write(char value);

        void Write(string? value);
        #endregion

        #region WriteLine
        void WriteLine();

        void WriteLine(char value);

        void WriteLine(string? value);
        #endregion

        #region Flush
        void Flush();

        Task FlushAsync();

        Task FlushAsync(CancellationToken cancellationToken);
        #endregion

        void Close();
    }
}