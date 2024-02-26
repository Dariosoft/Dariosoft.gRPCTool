using System.Text;

namespace Dariosoft.gRPCTool.Accessories
{
    public interface ITextWriter : IDisposable, IAsyncDisposable
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

    class TextWriter(StringBuilder content) : ITextWriter
    {
        public void Close() { }

        public void Dispose() { }

        public ValueTask DisposeAsync() => ValueTask.CompletedTask;

        public void Flush() { }

        public Task FlushAsync() => Task.CompletedTask;

        public Task FlushAsync(CancellationToken cancellationToken) => Task.CompletedTask;

        public void Write(char value) => content.Append(value);

        public void Write(string? value) => content.Append(value);

        public void WriteLine() => content.AppendLine();

        public void WriteLine(char value) => content.AppendLine(value.ToString());

        public void WriteLine(string? value) => content.AppendLine(value);
    }
}
